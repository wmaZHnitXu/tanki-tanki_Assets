using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTankPresenter : MonoBehaviour
{
    public Tank tank;
    [SerializeField] float camSpeed;
    [SerializeField] Transform camTrans;
    [SerializeField] Transform turret;
    [SerializeField] Transform body;
    Level level;


    void Start() {
        level = new Level(32, 32);
        tank = new Tank(new Turret(), PlayerController.instance);
        level.AddEntity(tank);
    }

    void Update()
    {
        level.DoUpdate(Time.deltaTime);

        transform.position = tank.position;
        turret.localRotation = Quaternion.Euler(0,0, tank.turret.yaw);
        body.localRotation = Quaternion.Euler(0,0, MathUtil.GetAngleFromVec(tank.velocity));

        Vector3 camTarget = new Vector3(transform.position.x, transform.position.y, camTrans.position.z);
        camTrans.position = Vector3.Lerp(camTrans.position, camTarget, camSpeed * Time.deltaTime);
    }
}
