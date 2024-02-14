using UnityEngine;

public class Bullet : Entity
{
    private Vector2 velocity;
    private float lifetime;
    private float maxLifetime = 2.0f;
    private float damage = 75.0f;
    private Entity owner;

    public Bullet(Level level, Vector2 position, Vector2 velocity, Entity owner) : base(level) {
        this.velocity = velocity;
        this.position = position;
        this.owner = owner;
        lifetime = 0.0f;
    }
    public override void Update(float delta)
    {
        Vector2 nextPos = position + velocity * delta;
        Entity e = null;
        if ((e = level.TraceLine(CanGoThrough, position, nextPos, out nextPos)) != null) {

            if (e is DestructibleEntity) {
                var d = e as DestructibleEntity;
                d.Damage(damage + Random.Range(0.0f, 1.0f) * damage, nextPos);
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

    private bool CanGoThrough(CollideableEntity entity) {
        if (entity == owner) return true;
        return false;
    }
}
