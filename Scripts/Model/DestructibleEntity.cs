using UnityEngine;

public abstract class DestructibleEntity : Entity
{
    private float _health;
    public float health {
        get => _health;
        protected set {
            _health = health;
        }
    }
    protected Collider _collider;
    public Collider collider {
        get => _collider;
        set {
            _collider = value;
        }
    }

    public abstract void Damage(float damage);

    public abstract void Damage(float damage, Vector2 hitPos);
}
