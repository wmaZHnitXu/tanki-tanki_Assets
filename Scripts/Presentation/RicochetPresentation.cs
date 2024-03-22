using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetPresentation : EntityPresentation
{
    [SerializeField] Transform turret;
    private RicochetTurret ricochet;

    public override void SetTargetAndUpdate(Entity target) {
        base.SetTargetAndUpdate(target);
        ricochet = target as RicochetTurret;
    }

    protected override void Update() {
        turret.rotation = Quaternion.Euler(0,0, ricochet.yaw);
        transform.position = ricochet.position;
    }
}
