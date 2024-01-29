using UnityEngine;

public class Tank : DestructibleEntity
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
    public Vector2 velocity;
    private IController controller;
    public override void Update(Level level, float delta) {

        velocity -= velocity * 1f * Time.deltaTime;
        velocity += controller.GetMoveDirection() * acceleration * delta;
        if (velocity.magnitude > speed) velocity = velocity.normalized * speed;

        position = position + velocity;

        turret.yaw = controller.GetLookAngle();
    }

    public Tank(Turret turret, IController controller) {
        this.turret = turret;
        turret.owner = this;
        this.controller = controller;
        health = 100.0f;
        acceleration = 0.06f;
        speed = 0.02f;
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
