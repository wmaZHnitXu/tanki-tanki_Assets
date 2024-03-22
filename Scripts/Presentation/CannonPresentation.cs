using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPresentation : EntityPresentation
{
    [SerializeField] Transform turret;
    private CannonTurret cannon;

    public override void SetTargetAndUpdate(Entity target) {
        base.SetTargetAndUpdate(target);
        cannon = target as CannonTurret;
    }

    protected override void Update() {
        turret.localRotation = Quaternion.Euler(0,0, cannon.yaw);
        transform.position = cannon.position;
    }
}
