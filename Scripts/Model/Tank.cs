using System.Collections.Generic;
using UnityEngine;

public class Tank : DestructibleEntity, IPushable
{
    private Turret _turret;
    public Turret turret {
        get => _turret;
        protected set {
            _turret = value;
        }
    }
    public float speed;
    public float acceleration;

    private Vector2 _velocity;
    public Vector2 velocity {
        get => _velocity;
        set {
            _velocity = value;
        }
    }
    private IController controller;
    private float shootCd;
    private float lookAngle;
    private float maxShootCd = 0.01f;
    private int team;
    public override void Update(Level level, float delta) {

        velocity -= velocity * 5.0f * Time.deltaTime;
        if (controller != null) {
            velocity += controller.GetMoveDirection() * acceleration * delta;
        }
        if (velocity.magnitude > speed) velocity = velocity.normalized * speed;

        position = position + velocity * delta;

        if (controller != null) {
            lookAngle = controller.GetLookAngle();
        }
        turret.yaw = lookAngle;

        shootCd -= delta;
        if (shootCd <= 0f && controller != null) {
            float x = Mathf.Cos((lookAngle + 90.0f) * Mathf.Deg2Rad);
            float y = Mathf.Sin((lookAngle + 90.0f) * Mathf.Deg2Rad);
            float cannonLength = 1.0f;
            float bulletSpeed = 50.0f;
            Vector2 bulletOriginPos = position + (new Vector2(x, y) * cannonLength);
            Bullet bullet = new Bullet(bulletOriginPos, new Vector2(x, y) * bulletSpeed, this);

            level.AddEntity(bullet);
            shootCd = maxShootCd;
        }
    }

    public Tank(Turret turret, IController controller) {
        colliders = new List<Collider> {
            new CircleCollider(this, 0.5f)
        };
        this.turret = turret;
        turret.owner = this;
        this.controller = controller;
        health = 100.0f;
        acceleration = 50.0f;
        speed = 10f;
    }

    public void AddVelocity(Vector2 velocity) {
        this.velocity += velocity;
    }

    public override void Damage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Damage(float damage, Vector2 hitPos)
    {
        throw new System.NotImplementedException();
    }
}
