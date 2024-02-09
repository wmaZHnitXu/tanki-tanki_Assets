using Unity.Mathematics;
using UnityEngine;

public class PlayerControllerPC : PlayerController
{
    public override bool IsFiring()
    {
        return Input.GetMouseButton(0);
    }

    void Start() {
        current = this;
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


        Vector2 mouseDir = Input.mousePosition;
        mouseDir = new Vector2(mouseDir.x - Screen.width / 2, mouseDir.y - Screen.height / 2);
        angle = MathUtil.GetAngleFromVec(mouseDir);
    }

    
}
