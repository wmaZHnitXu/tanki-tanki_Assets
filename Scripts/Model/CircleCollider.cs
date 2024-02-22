using UnityEngine;

public class CircleCollider : Collider
{
    private float _radius;
    public float radius {
        get => _radius;
        set => _radius = value;
    }

    public CircleCollider(Entity owner, float radius) {
        this.owner = owner;
        this.radius = radius;
    }

    public override float GetPointDepth(Vector2 point)
    {
        return radius - Vector2.Distance(point, owner.position);
    }

    public override Vector2 GetNormal(Vector2 point)
    {
        throw new System.NotImplementedException();
    }
}
