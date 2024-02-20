using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShilkaPresentation : EntityPresentation
{
    [SerializeField] Transform turret;
    private ShilkaTurret shilka;

    public override void SetTargetAndUpdate(Entity target) {
        base.SetTargetAndUpdate(target);
        shilka = target as ShilkaTurret;
    }

    protected override void Update() {
        turret.localRotation = Quaternion.Euler(0,0, shilka.yaw);
        transform.position = shilka.position;
    }
}
