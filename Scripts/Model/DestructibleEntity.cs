using UnityEngine;

public abstract class DestructibleEntity : Entity
{
    private float _health;
    public float health {
        get => _health;
        private set {
            _health = health;
        }
    }
    private Collider _collider;
    public Collider collider {
        get => _collider;
        set {
            _collider = value;
        }
    }

    public abstract void Damage(float damage);

    public abstract void Damage(float damage, Vector2 hitPos);
}
