using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTurret : Turret
{
    private float shootCd;
    private float maxShootCd = 0.5f;

    public ShotgunTurret(Level level, Tank owner, TankInfo info) : base(level, owner, info)
    {

    }
    public override void Update(float delta)
    {
        shootCd -= delta;
        if (shootCd <= 0f && isFiring)
        {
            float cannonLength = 1.0f;

            float bulletSpeed = 20.0f;

            float angle = 10.0f;
            
            float x = Mathf.Cos((rotation + 90.0f) * Mathf.Deg2Rad);
            float y = Mathf.Sin((rotation + 90.0f) * Mathf.Deg2Rad);

            float xL = Mathf.Cos((rotation + 90.0f + angle) * Mathf.Deg2Rad);
            float yL = Mathf.Sin((rotation + 90.0f + angle) * Mathf.Deg2Rad);

            float xR = Mathf.Cos((rotation + 90.0f - angle) * Mathf.Deg2Rad);
            float yR = Mathf.Sin((rotation + 90.0f - angle) * Mathf.Deg2Rad);

            Vector2 bulletOriginPos = position + (new Vector2(x, y) * cannonLength);
            Bullet bullet = new Bullet(level, bulletOriginPos, new Vector2(x, y) * bulletSpeed + velocity, owner);

            Vector2 leftBulletPos = position + (new Vector2(xL, yL) * cannonLength);
            Bullet bulletL = new Bullet(level, leftBulletPos, new Vector2(xL, yL) * bulletSpeed + velocity, owner);

            Vector2 rightBulletPos = position + (new Vector2(xR, yR) * cannonLength);
            Bullet bulletR = new Bullet(level, rightBulletPos, new Vector2(xR, yR) * bulletSpeed + velocity, owner);

            shootCd = maxShootCd;
        }
    }
}
 