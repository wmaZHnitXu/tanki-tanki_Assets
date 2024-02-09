using UnityEngine;

public class Bullet : Entity
{
    private Vector2 velocity;
    private float lifetime;
    private float maxLifetime = 2.0f;
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

            
            hitFlag = true;
            //Debug.Log((e as Tank).position);
        }

        position = nextPos;
        lifetime += delta;

        if (lifetime > maxLifetime || hitFlag) {
            Kill();
        }
    }
}
