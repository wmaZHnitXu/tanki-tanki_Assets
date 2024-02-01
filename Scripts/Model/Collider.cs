using UnityEngine;

public abstract class Collider
{
    protected Entity owner;
    private bool _hitBoxOnly;
    public bool hitBoxOnly {
        get => _hitBoxOnly;
        set {
            _hitBoxOnly = value;
        }
    }

    public Vector2 position {
        get {
            return owner.position;
        }
    }
    public abstract float GetPointDepth(Vector2 point);
}
