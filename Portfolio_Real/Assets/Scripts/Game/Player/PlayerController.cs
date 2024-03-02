using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerMove playerMove;
    [SerializeField]
    PlayerJump playerJump;

    float horizontalDir;
    bool isLeftKeyDown;
    bool isRightKeyDown;

    void Update()
    {
        horizontalDir = GetHorizontalKey();
        if (InputManager.GetKeyDown(InputKeys.Jump))
        {
            playerJump.Jump();
        }
    }

    void FixedUpdate()
    {
        playerMove.Move(horizontalDir);
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
}
