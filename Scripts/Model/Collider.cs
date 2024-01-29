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
    public abstract float getPointDepth(Vector2 point);
}
