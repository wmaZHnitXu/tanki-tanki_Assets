using UnityEngine;

public class CircleCollider : Collider
{
    private float _radius;
    public float radius {
        get => _radius;
        set {
            _radius = value;
        }
    }
    public override float getPointDepth(Vector2 point)
    {
        return radius - Vector2.Distance(point, owner.position);
    }
}
