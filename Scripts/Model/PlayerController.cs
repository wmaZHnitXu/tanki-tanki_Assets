using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    private Vector2 moveDirection;
    private Tank tank;

    public static PlayerController instance;

    void Start() {
        instance = this;
    }

    void Update() {
        moveDirection = new Vector2(0f, 0f);

        if (Input.GetKey(KeyCode.D)) {
            moveDirection = new Vector2(moveDirection.x + 1.0f, moveDirection.y);
        }
        if (Input.GetKey(KeyCode.A)) {
            moveDirection = new Vector2(moveDirection.x - 1.0f, moveDirection.y);
        }
        if (Input.GetKey(KeyCode.W)) {
            moveDirection = new Vector2(moveDirection.x, moveDirection.y + 1.0f);
        }
        if (Input.GetKey(KeyCode.S)) {
            moveDirection = new Vector2(moveDirection.x, moveDirection.y - 1.0f);
        }

        moveDirection = moveDirection.normalized;
    }

    public float GetLookAngle() {
        Vector2 mouseDir = Input.mousePosition;
        mouseDir = new Vector2(mouseDir.x - Screen.width / 2, mouseDir.y - Screen.height / 2);
        return MathUtil.GetAngleFromVec(mouseDir);
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }
}
