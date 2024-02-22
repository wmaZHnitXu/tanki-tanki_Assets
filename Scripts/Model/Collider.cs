using UnityEngine;

public abstract class Collider : IPosAndRot
{
    protected Entity owner;
    private bool _hitBoxOnly;
    public bool hitBoxOnly {
        get => _hitBoxOnly;
        set => _hitBoxOnly = value;
    }

    public Vector2 position {
        get {
            return owner.position;
        }
    }

    virtual public float rotation {
        get {
            return 0.0f;
        }
    }

    public abstract float GetPointDepth(Vector2 point);

    public abstract Vector2 GetNormal(Vector2 point);
}
