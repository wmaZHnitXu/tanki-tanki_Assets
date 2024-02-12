using UnityEngine;

public class Bullet : Entity
{
    private Vector2 velocity;
    private float lifetime;
    private float maxLifetime = 2.0f;
    private float damage = 10.0f;
    private Entity owner;

    public Bullet(Vector2 position, Vector2 velocity, Entity owner) {
        this.velocity = velocity;
        this.position = position;
        this.owner = owner;
        lifetime = 0.0f;
    }
    public override void Update(Level level, float delta)
    {
        Vector2 nextPos = position + velocity * delta;
        bool hitFlag = false;
        Entity e = null;
        if ((e = level.TraceLine(owner, position, nextPos, out nextPos)) != null) {

            if (e is DestructibleEntity) {
                var d = e as DestructibleEntity;
                d.Damage(damage, position);
                Kill();
                return;
            }
        }

        position = nextPos;
        lifetime += delta;

        if (lifetime > maxLifetime) {
            Kill();
        }
    }
}
