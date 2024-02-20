using UnityEngine;

public interface IController
{
    public Vector2 GetMoveDirection();
    public float GetLookAngle(Vector2 position);
    public bool IsFiring();
}
