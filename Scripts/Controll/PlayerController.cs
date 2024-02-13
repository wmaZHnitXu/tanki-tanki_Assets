using UnityEngine;

public abstract class PlayerController : MonoBehaviour, IController
{
    protected Vector2 moveDirection;
    protected float angle;
    public Tank tank;
    public static PlayerController current;

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public virtual Vector2 GetLookVector() {
        return new Vector2(Mathf.Cos((angle + 90.0f) * Mathf.Deg2Rad), Mathf.Sin((angle + 90.0f) * Mathf.Deg2Rad));
    }

    public abstract bool IsFiring();

    public abstract float GetLookAngle(Vector2 position);

}
