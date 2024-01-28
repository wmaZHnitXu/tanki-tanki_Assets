using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    Vector2 moveDirection;
    [SerializeField] Vector2 velocity;
    [SerializeField] Transform camTrans;
    [SerializeField] Transform turret;
    [SerializeField] Transform body;
    [SerializeField] float camSpeed;

    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    void Start()
    {
        
    }

    void Update()
    {
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

        velocity -= velocity * 1f * Time.deltaTime;
        velocity += moveDirection * acceleration * Time.deltaTime;
        if (velocity.magnitude > maxSpeed) velocity = velocity.normalized * maxSpeed;

        transform.Translate(velocity);

        Vector3 camTarget = new Vector3(transform.position.x, transform.position.y, camTrans.position.z);
        camTrans.position = Vector3.Lerp(camTrans.position, camTarget, camSpeed * Time.deltaTime);

        turret.localRotation = Quaternion.Euler(0,0, GetLookAngle());
        body.localRotation = Quaternion.Euler(0,0, GetAngleFromVec(velocity));
    }

    public float GetLookAngle() {
        Vector2 mouseDir = Input.mousePosition;
        mouseDir = new Vector2(mouseDir.x - Screen.width / 2, mouseDir.y - Screen.height / 2);
        return GetAngleFromVec(mouseDir);
    }

    public float GetAngleFromVec(Vector2 vec) {
        vec = vec.normalized;
        return -math.atan2(vec.x, vec.y) * Mathf.Rad2Deg;
    }
}
