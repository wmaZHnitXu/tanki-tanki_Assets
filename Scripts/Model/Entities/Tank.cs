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
    private float lookAngle;
    private int team;
    public override void Update(float delta) {

        velocity -= velocity * 5.0f * Time.deltaTime;
        if (controller != null) {
            velocity += controller.GetMoveDirection() * acceleration * delta;
        }
        if (velocity.magnitude > speed) velocity = velocity.normalized * speed;

        position = position + velocity * delta;
        if (velocity != Vector2.zero)
            rotation = MathUtil.GetAngleFromVec(velocity);

        if (controller != null) {
            lookAngle = controller.GetLookAngle(position);
        }

        //TURRET
        turret.yaw = lookAngle;
        if (controller != null) {
            turret.isFiring = controller.IsFiring();
        }
    }

    public Tank(Level level, TankInfo info, Vector2 position, IController controller) : base(level) {

        this.info = info;

        this.colliders = new List<Collider> {
            new CircleCollider(this, 0.5f),
            new RectCollider(this, 1.0f, 1.0f, true)
        };

        var turret = Turret.Create(level, this, info);        

        this.turret = turret;
        this.position = position;
        this.controller = controller;
        this.maxHealth = 200.0f;
        this.health = this.maxHealth;
        this.acceleration = 50.0f;
        this.speed = 10f;

        this.team = (controller is PlayerController) ? 0 : 1;
    }

    public void AddVelocity(Vector2 velocity) {
        this.velocity += velocity;
    }

    public override bool MustBeDestroyedForLevelToEnd()
    {
        return !(controller is PlayerController);
    }

    protected override void Death() {
        Debug.Log("Killed");
        Debug.Log(turret.rotation);
        //new TankCorpse(level, info, position, velocity, rotation, turret.rotation);
    }

    public bool CanPush(IPushable pushable)
    {
        if (pushable is CollectableEntity) return false;
        return true;
    }

    public override float GetOuterRadius() {
        return 1.4142f; //sqrt(2)
    }
}
