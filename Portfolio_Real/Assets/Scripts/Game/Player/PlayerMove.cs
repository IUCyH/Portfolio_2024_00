using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float runSpeed;
    float currSpeed;

    void Start()
    {
        currSpeed = walkSpeed;
    }

    public void Move(float dir)
    {
        var nextVector = Vector3.right * dir;

        transform.position += currSpeed * Time.deltaTime * nextVector;
    }
}
