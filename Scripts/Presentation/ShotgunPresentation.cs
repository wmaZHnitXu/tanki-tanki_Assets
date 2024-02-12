using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPresentation : EntityPresentation
{
    [SerializeField] Transform turret;
    private ShotgunTurret shotgun;

    public override void SetTargetAndUpdate(Entity target) {
        base.SetTargetAndUpdate(target);
        shotgun = target as ShotgunTurret;
    }

    void Update() {
        turret.localRotation = Quaternion.Euler(0,0, shotgun.yaw);
        transform.position = shotgun.position;
    }
}
