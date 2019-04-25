using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalController : controller
{

    private void Awake()
    {
        //arriveMaxHeight = false;
}
    void FixedUpdate()
    {
        base.FixedUpdate();
        switch (status)
        {
            case 1:
                verticalSpeed = 150f;
                break;
            case 0:
                verticalSpeed = 0f;
                break;
            case 3:
                verticalSpeed -= g;
                break;
        }
        if (verticalSpeed < -100f)
            verticalSpeed = -100f;
    }
    //private void Fall()
    //{
    //    if (isTransitionState)
    //    {
    //        movingVerticallyTime += Time.deltaTime * 15;
    //        isTransitionState = false;
    //    }
    //    else
    //    {
    //        movingVerticallyTime += Time.deltaTime;
    //    }
    //    if (movingVerticallyTime < maxFallingTime)
    //    {
    //        transform.position = transform.position + Vector3.down * g * movingVerticallyTime * movingVerticallyTime * Time.deltaTime;
    //    }
    //    else
    //    {
    //        transform.position = transform.position + Vector3.down * g * maxFallingTime * maxFallingTime * Time.deltaTime;
    //    }
    //    isGrounded = false;
    //}
}
