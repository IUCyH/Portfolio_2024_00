using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    Transform feetPos;
    
    [SerializeField]
    float jumpForce;
    float currJumpForce;
    [SerializeField]
    float jumpDecreaseValue;
    [SerializeField]
    float radiusOfCircleDetectingGround;
    bool jump;

    void Start()
    {
        StartCoroutine(Coroutine_Update());
    }

    IEnumerator Coroutine_Update()
    {
        while (true)
        {
            if (jump)
            {
                transform.position += new Vector3(0f, currJumpForce * Time.deltaTime, 0f);
                currJumpForce -= jumpDecreaseValue;
                
                if (Mathf.Approximately(currJumpForce, 0f) || currJumpForce < 0f)
                {
                    jump = false;
                }
            }
            
            yield return null;
        }
    }
    
    public void Jump()
    {
        if (!CanJump()) return;

        currJumpForce = jumpForce;
        jump = true;
    }

    bool CanJump()
    {
        var detectedCollider = Physics2D.OverlapCircle(feetPos.position, radiusOfCircleDetectingGround, 1 << LayerMask.NameToLayer("Ground"));

        return !ReferenceEquals(detectedCollider, null);
    }
}
