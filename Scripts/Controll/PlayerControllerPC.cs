using UnityEngine;

public class PlayerControllerPC : PlayerController
{
    public override bool IsFiring()
    {
        return Input.GetMouseButton(0);
    }

    Vector2 mouseDir;

    [SerializeField] Camera cam;

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


        mouseDir = Input.mousePosition;
        mouseDir = new Vector2(mouseDir.x - Screen.width / 2, mouseDir.y - Screen.height / 2);
    }

    public override Vector2 GetLookVector()
    {
        float width = Screen.width;
        float height = Screen.height;
        float wSqr =  width * width;
        float hSqr = height * height;
        return mouseDir / Mathf.Sqrt(wSqr + hSqr);
    }

    public override float GetLookAngle(Vector2 position)
    {
        Vector3 worldMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        angle = MathUtil.GetAngleFromVec((Vector2)worldMousePos - position);
        return angle;
    }
}
