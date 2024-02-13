using UnityEngine;

public abstract class DestructibleEntity : CollideableEntity
{
    private float _health;
    public float health {
        get => _health;
        protected set {
            _health = value;
            if (health < 0f && !isDead) {
                Kill();
            }
        }
    }
    public float maxHealth;

    public delegate void OnDamage(DestructibleEntity destructible, float amount, Vector2 from);
    public event OnDamage OnDamageEvent;

    public virtual void Damage(float damage) {
        health -= damage;
        OnDamageEvent?.Invoke(this, damage, position);

    }

    public virtual void Damage(float damage, Vector2 hitPos) {
        health -= damage;
        OnDamageEvent?.Invoke(this, damage, hitPos);
    }
}
