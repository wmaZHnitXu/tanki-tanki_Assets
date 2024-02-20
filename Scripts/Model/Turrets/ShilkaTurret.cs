 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShilkaTurret : Turret
{
    private float shootCd;
    private float maxShootCd = 0.02f;
    public ShilkaTurret(Level level, Tank owner, TankInfo info) : base(level, owner, info)
    {

    }

    public override void Update(float delta)
    {
        shootCd -= delta;
        if (shootCd <= 0f && isFiring) {
            float randomAngle = Random.Range(-5.0f, 5.0f);
            float x = Mathf.Cos((rotation + randomAngle + 90.0f) * Mathf.Deg2Rad);
            float y = Mathf.Sin((rotation + randomAngle + 90.0f) * Mathf.Deg2Rad);
            float cannonLength = 1.0f;
            float bulletSpeed = 50.0f;
            Vector2 bulletOriginPos = position + (new Vector2(x, y) * cannonLength);
            Bullet bullet = new Bullet(level, bulletOriginPos, new Vector2(x, y) * bulletSpeed + velocity, owner);

            shootCd = maxShootCd;
        }
    }
}
