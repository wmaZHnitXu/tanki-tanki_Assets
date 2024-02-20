using System.Collections.Generic;
using UnityEngine;

public class Tank : DestructibleEntity, IPushable
{
    public readonly TankInfo info;
    private Turret _turret;
    public Turret turret {
        get => _turret;
        protected set => _turret = value;
    }
    public float speed;
    public float acceleration;

    private Vector2 _velocity;
    public Vector2 velocity {
        get => _velocity;
        set => _velocity = value;
    }
    private IController controller;
    private float shootCd;
    private float lookAngle;
    private float maxShootCd = 0.02f;
    private int team;
    public override void Update(Level level, float delta) {

        velocity -= velocity * 5.0f * Time.deltaTime;
        if (controller != null) {
            velocity += controller.GetMoveDirection() * acceleration * delta;
        }
        if (velocity.magnitude > speed) velocity = velocity.normalized * speed;

        position = position + velocity * delta;
        if (velocity != Vector2.zero)
            rotation = MathUtil.GetAngleFromVec(velocity);

        if (controller != null) {
            lookAngle = controller.GetLookAngle();
        }

        //TURRET
        turret.yaw = lookAngle;
        if (controller != null) {
            turret.isFiring = controller.IsFiring();
        }
    }

    public Tank(TankInfo info, IController controller, Level level) {
        this.colliders = new List<Collider> {
            new CircleCollider(this, 0.5f),
            new RectCollider(this, 1.0f, 1.0f, true)
        };

        var turret = new RicochetTurret(this, info);
        level.AddEntity(turret);
        

        this.turret = turret;
        this.controller = controller;
        this.health = 100.0f;
        this.acceleration = 50.0f;
        this.speed = 10f;
    }

    public void AddVelocity(Vector2 velocity) {
        this.velocity += velocity;
    }
}
