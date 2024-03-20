using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RicochetBullet : Entity
{
    public Vector2 velocity;
    private float lifetime;
    private float maxLifetime = 20.0f;
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
        var to = nextPos;
        if ((e = level.TraceLine(CanGoThrough, position, nextPos, out nextPos, out normal)) != null) {
        
            float remainingDistance = Vector2.Distance(nextPos, to);
            velocity = Vector2.Reflect(velocity, normal);

            if (e is DestructibleEntity) {
                var destr = e as DestructibleEntity;
                destr.Damage(250f, nextPos);
            }
            
            lifetime += delta;
            nextPos += velocity.normalized * remainingDistance;
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
