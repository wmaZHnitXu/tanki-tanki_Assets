using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShilkaTurret : Turret
{
    public ShilkaTurret(Tank owner, TankInfo info) : base(owner, info)
    {

    }

    public override int GetAmmoCount()
    {
        throw new System.NotImplementedException();
    }

    public override int GetMaxAmmo()
    {
        throw new System.NotImplementedException();
    }
}
