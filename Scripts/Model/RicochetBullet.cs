using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RicochetBullet : Entity
{
    private Vector2 velocity;
    private float lifetime;
    private float maxLifetime = 2.0f;
    private Entity owner;

    public RicochetBullet(Level level, Vector2 position, Vector2 velocity, Entity owner) : base(level) {
        this.velocity = velocity;
        this.position = position;
        this.owner = owner;
        lifetime = 0.0f;
    }

    public override void Update(float delta)
    {
        Vector2 nextPos = position + velocity * delta;
        Vector2 normal = Vector2.zero;
        Entity e = null;
        if ((e = level.TraceLine(CanGoThrough, position, nextPos, out nextPos, out normal)) != null) {
            velocity = Vector2.Reflect(velocity, normal);
            lifetime += delta;
        }

        else
        {
            position = nextPos;
            lifetime += delta;
        }

        if (lifetime > maxLifetime) {
            Kill();
        }
    }

    private bool CanGoThrough(CollideableEntity entity) {
        if (entity == owner) return true;
        return false;
    }
}
