using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingDummyController : IController
{
    float speed = 1f;
    public float GetLookAngle(Vector2 position)
    {
        return Time.time * speed * Mathf.Rad2Deg - 90.0f;
    }

    public Vector2 GetMoveDirection()
    {
        
        return new Vector2(Mathf.Cos(Time.time * speed), Mathf.Sin(Time.time * speed));
    }

    public bool IsFiring()
    {
        return true;
    }
}
