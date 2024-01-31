using UnityEngine;

public class Bullet : Entity
{
    private Vector2 velocity;
    private float lifetime;
    private float maxLifetime = 2.0f;

    public Bullet(Vector2 position, Vector2 velocity) {
        this.velocity = velocity;
        this.position = position;
        lifetime = 0.0f;
    }
    public override void Update(Level level, float delta)
    {
        position += velocity * delta;
        lifetime += delta;

        if (lifetime > maxLifetime) {
            Kill();
        }
    }
}
