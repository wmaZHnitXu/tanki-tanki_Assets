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
        Entity e = null;
        if ((e = level.TraceLine(CanGoThrough, position, nextPos, out nextPos)) != null) {
            Debug.Log(nextPos);
            Vector2 reflectDir2D = nextPos.normalized;
            Debug.Log(reflectDir2D);
            float rot = 90 - Mathf.Atan2(reflectDir2D.y, reflectDir2D.x) * Mathf.Rad2Deg;
            velocity = Vector2.Reflect(velocity, new Vector2(1, 0));

            /*
            Vector2 reflectDir = Vector2.Reflect(position, nextPos.normalized);
            float rot = 90 - Mathf.Atan2(reflectDir.x, reflectDir.y) * Mathf.Rad2Deg;
            position = new Vector2(0, rot);
            lifetime += delta;*/
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
