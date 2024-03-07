using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField]
    float walkSpeed;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void Move(float dir)
    {
        var nextVector = Vector3.right * dir;

        transform.position += walkSpeed * Time.deltaTime * nextVector;
    }

    public void ChangeToMoveState(float dir)
    {
        if (Mathf.Approximately(dir, 0f)) return;
     
        playerController.ChangeStateAndPlayAnimation(PlayerState.Move);
    }
}
