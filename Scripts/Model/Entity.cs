using UnityEngine;

public abstract class Entity
{
    private Vector2 _position;
    public Vector2 position {
        get => _position;
        protected set {
            _position = value;
        }
    }

    private float _rotation;
    public float rotation {
        get => _rotation;
        protected set {
            _rotation = value;
        }
    }

    protected bool isDead;

    public delegate void OnDeath(Entity sender);
    public event OnDeath OnDeathEvent;

    public abstract void Update(Level level, float delta);

    public void Kill() {
        if (isDead) return;
        isDead = true;
        OnDeathEvent?.Invoke(this);
        OnDeathEvent = null;
        Death();
    }

    protected virtual void Death() {

    }
}
