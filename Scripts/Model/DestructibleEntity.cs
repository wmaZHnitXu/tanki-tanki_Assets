using UnityEngine;

public abstract class DestructibleEntity : CollideableEntity
{
    private float _health;
    public float health {
        get => _health;
        protected set => _health = health;
    }

    public abstract void Damage(float damage);

    public abstract void Damage(float damage, Vector2 hitPos);
}
