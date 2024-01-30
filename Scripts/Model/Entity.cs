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

    public delegate void OnDeath(Entity sender);
    public event OnDeath OnDeathEvent;

    public virtual void Update(Level level, float delta) {

    }

    public virtual void Kill() {
        OnDeathEvent?.Invoke(this);
        OnDeathEvent = null;
    }
}
