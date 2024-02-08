using UnityEngine;

public abstract class Entity
{
    private Vector2 _position;
    public virtual Vector2 position {
        get => _position;
        set => _position = value;   
    }

    private float _rotation;
    public float rotation {
        get => _rotation;
        protected set => _rotation = value;
    }

    protected bool isDead;

    public delegate void OnDeath(Entity sender);
    public event OnDeath OnDeathEvent;

    public abstract void Update(Level level, float delta);

    public void Kill(bool silent = false) {
        if (isDead) return;
        isDead = true;
        OnDeathEvent?.Invoke(this);
        OnDeathEvent = null;
        ObligatoryOnRemove();
        if (!silent) {
            Death();
        }
    }

    protected virtual void ObligatoryOnRemove() {

    }

    protected virtual void Death() {

    }
}
