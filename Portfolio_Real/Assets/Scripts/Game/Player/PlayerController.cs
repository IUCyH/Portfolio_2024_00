using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    None = -1,
    OnGround,
    Move,
    JumpStart,
    OnAir,
    JumpEnd
}

public class PlayerController : MonoBehaviour
{
    PlayerAnimationController playerAnimCtr;
    [SerializeField]
    PlayerSkill playerSkill;
    [SerializeField]
    PlayerMove playerMove;
    [SerializeField]
    PlayerJump playerJump;
    [SerializeField]
    Transform feetPos;
    
    PlayerState currentState;
    int groundLayer;
    [SerializeField]
    float radiusOfCircleDetectingGround;
    float horizontalDir;
    bool isLeftKeyDown;
    bool isRightKeyDown;

    void Start()
    {
        playerAnimCtr = new PlayerAnimationController(GetComponent<Animator>());
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        horizontalDir = GetHorizontalKey();
        
        playerJump.Jump(InputManager.GetKeyDown(InputKeys.Jump));
        playerJump.ChangeToJumpState();
        
        playerSkill.CheckAnySkillKeyDown();
        playerSkill.ExecuteSkill();
        playerSkill.CalculateSKillCoolDowns();
        
        ChangeToIdleIfOnGround();
    }

    void FixedUpdate()
    {
        playerMove.Move(horizontalDir);
        playerMove.ChangeToMoveState(horizontalDir);
        SetPlayerForward();
    }
    
    public bool OnGround()
    {
        var detectedCollider = Physics2D.OverlapCircle(feetPos.position, radiusOfCircleDetectingGround, groundLayer);

        return !ReferenceEquals(detectedCollider, null);
    }

    public void ChangeStateAndPlayAnimation(PlayerState state)
    {
        if (currentState == state) return;
        
        currentState = state;
        
        PlayAnimation();
    }

    float GetHorizontalKey()
    {
        if (InputManager.GetKeyDown(InputKeys.Left))
        {
            isLeftKeyDown = true;
        }

        if (InputManager.GetKeyUp(InputKeys.Left))
        {
            isLeftKeyDown = false;
        }

        if (InputManager.GetKeyDown(InputKeys.Right))
        {
            isRightKeyDown = true;
        }

        if (InputManager.GetKeyUp(InputKeys.Right))
        {
            isRightKeyDown = false;
        }

        if (isLeftKeyDown && isRightKeyDown) return 0f;
        if (isLeftKeyDown) return -1f;
        if (isRightKeyDown) return 1f;

        return 0f;
    }

    void PlayAnimation()
    {
        switch (currentState)
        {
            case PlayerState.OnGround:
                playerAnimCtr.Play(PlayerMotion.Idle);
                break;

            case PlayerState.Move:
                playerAnimCtr.Play(PlayerMotion.Move);
                break;

            case PlayerState.JumpStart:
                playerAnimCtr.Play(PlayerMotion.Jump);
                break;

            case PlayerState.OnAir:
                playerAnimCtr.Play(PlayerMotion.OnAir);
                break;

            case PlayerState.JumpEnd:
                playerAnimCtr.Play(PlayerMotion.JumpEnd);
                break;
        }
    }

    void SetPlayerForward()
    {
        if (Mathf.Approximately(horizontalDir, 0f)) return;
        
        var scale = Vector3.one;

        scale.x *= horizontalDir;
        transform.localScale = scale;
    }

    void ChangeToIdleIfOnGround()
    {
        if (!OnGround() || !Mathf.Approximately(horizontalDir, 0f)) return;
        
        ChangeStateAndPlayAnimation(PlayerState.OnGround);
    }
}
