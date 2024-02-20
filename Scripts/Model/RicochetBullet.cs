using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RicochetBullet : Entity
{
    private Vector2 velocity;
    private float lifetime;
    private float maxLifetime = 2.0f;
    private Entity owner;

    public RicochetBullet(Vector2 position, Vector2 velocity, Entity owner) {
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
            hitFlag = true;
        }

        if (hitFlag) 
        {
            Debug.Log(nextPos);
            Vector2 reflectDir2D = nextPos.normalized;
            Debug.Log(reflectDir2D);
            float rot = 90 - Mathf.Atan2(reflectDir2D.y, reflectDir2D.x) * Mathf.Rad2Deg;
            position = Vector2.Reflect(position, nextPos.normalized);

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
}
