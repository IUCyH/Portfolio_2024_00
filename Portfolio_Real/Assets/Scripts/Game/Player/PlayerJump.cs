using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    
    PlayerController playerController;
    
    [SerializeField]
    float jumpForce;
    float currJumpForce;
    [SerializeField]
    float jumpDecreaseValue;
    bool jump;
    
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        StartCoroutine(Coroutine_Update());
    }

    IEnumerator Coroutine_Update()
    {
        bool isPlayedJumpAnim = false;
        
        while (true)
        {
            if (jump)
            {
                transform.position += new Vector3(0f, currJumpForce * Time.deltaTime, 0f);
                currJumpForce -= jumpDecreaseValue;

                if (Mathf.Approximately(currJumpForce, 0f) || currJumpForce < 0f)
                {
                    isPlayedJumpAnim = false;
                    jump = false;
                    playerController.ChangeStateAndPlayAnimation(PlayerState.JumpEnd);
                }

                if (!isPlayedJumpAnim)
                {
                    isPlayedJumpAnim = true;
                    playerController.ChangeStateAndPlayAnimation(PlayerState.OnAir);
                }
            }

            yield return null;
        }
    }

    public void ChangeToJumpState()
    {
        if (!jump) return;
        
        playerController.ChangeStateAndPlayAnimation(PlayerState.JumpStart);
    }
    
    public void Jump(bool jumpKeyDown)
    {
        if (!jumpKeyDown || !playerController.OnGround()) return;

        currJumpForce = jumpForce;
        jump = true;
    }
}
