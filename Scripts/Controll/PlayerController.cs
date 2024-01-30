using UnityEngine;

public abstract class PlayerController : MonoBehaviour, IController
{
    protected Vector2 moveDirection;
    protected float angle;
    protected Tank tank;
    public static PlayerController current;

    public float GetLookAngle() {
        return angle;
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }
}
