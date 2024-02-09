using UnityEngine;

public class TankPresentation : EntityPresentation
{
    [SerializeField] float camSpeed;
    [SerializeField] Transform camTrans;
    [SerializeField] Transform turret;
    [SerializeField] Transform body;
    private Tank tank;
    Level level;

    public override void SetTargetAndUpdate(Entity target) {
        base.SetTargetAndUpdate(target);
        tank = target as Tank;
        target.OnDeathEvent += Dispose;
        transform.position = target.position;
    }

    void Update()
    {
        transform.position = tank.position;
        turret.localRotation = Quaternion.Euler(0,0, tank.turret.yaw);
        body.localRotation = Quaternion.Euler(0,0, tank.rotation);

        if (camTrans == null) return;
        Vector3 camTarget = new Vector3(transform.position.x, transform.position.y, camTrans.position.z);
        camTrans.position = Vector3.Lerp(camTrans.position, camTarget, camSpeed * Time.deltaTime);
    }
}
