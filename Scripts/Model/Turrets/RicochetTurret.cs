using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetTurret : Turret
{
    private float shootCd;
    private float maxShootCd = 0.5f;

    public RicochetTurret(Tank owner, TankInfo info) : base(owner, info)
    {

    }
    public override void Update(Level level, float delta)
    {
        shootCd -= delta;
        if (shootCd <= 0f && isFiring)
        {
            
            float x = Mathf.Cos((rotation + 90.0f) * Mathf.Deg2Rad);
            float y = Mathf.Sin((rotation + 90.0f) * Mathf.Deg2Rad);
            float cannonLength = 1.0f;
            float bulletSpeed = 30.0f;
            Vector2 bulletOriginPos = position + (new Vector2(x, y) * cannonLength);
            RicochetBullet bullet = new RicochetBullet(bulletOriginPos, new Vector2(x, y) * bulletSpeed, this);

            level.AddEntity(bullet);
            shootCd = maxShootCd;
        }
    }
}
 