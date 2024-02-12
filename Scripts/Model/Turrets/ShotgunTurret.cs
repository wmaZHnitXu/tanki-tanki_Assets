using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTurret : Turret
{
    private float shootCd;
    private float maxShootCd = 0.5f;

    public ShotgunTurret(Tank owner, TankInfo info) : base(owner, info)
    {

    }
    public override void Update(Level level, float delta)
    {
        shootCd -= delta;
        if (shootCd <= 0f && isFiring)
        {
            float cannonLength = 1.0f;
            float bulletSpeed = 10.0f;

            float angle = 20.0f;
            
            float x = Mathf.Cos((rotation + 90.0f) * Mathf.Deg2Rad);
            float y = Mathf.Sin((rotation + 90.0f) * Mathf.Deg2Rad);

            float xL = Mathf.Cos((rotation + 90.0f + angle) * Mathf.Deg2Rad);
            float yL = Mathf.Sin((rotation + 90.0f + angle) * Mathf.Deg2Rad);

            float xR = Mathf.Cos((rotation + 90.0f - angle) * Mathf.Deg2Rad);
            float yR = Mathf.Sin((rotation + 90.0f - angle) * Mathf.Deg2Rad);

            Vector2 bulletOriginPos = position + (new Vector2(x, y) * cannonLength);
            Bullet bullet = new Bullet(bulletOriginPos, new Vector2(x, y) * bulletSpeed, this);

            Vector2 leftBulletPos = position + (new Vector2(xL, yL) * cannonLength);
            Bullet bulletL = new Bullet(leftBulletPos, new Vector2(xL, yL) * bulletSpeed, this);

            Vector2 rightBulletPos = position + (new Vector2(xR, yR) * cannonLength);
            Bullet bulletR = new Bullet(rightBulletPos, new Vector2(xR, yR) * bulletSpeed, this);

            level.AddEntity(bullet);
            level.AddEntity(bulletL);
            level.AddEntity(bulletR);
            shootCd = maxShootCd;
        }
    }
}
 