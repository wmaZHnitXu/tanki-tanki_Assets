using System;
using UnityEngine;

public class RectCollider : Collider
{

    public enum Side : int {
        right,
        top,
        left,
        bottom
    }
    public float angle {
        get => owner.rotation;
    }
    private float _width;
    private float _height;
    public float width
    {
        get => _width;
        set
        {
            _width = value;
        }
    }
    public float height
    {
        get => _height;
        set
        {
            _height = value;
        }
    }

    public RectCollider(Entity owner, float width, float height) {
        this.owner = owner;
        this.width = width;
        this.height = height;
    }

    public RectCollider(Entity owner, float width, float height, bool isHitbox) {
        this.owner = owner;
        this.width = width;
        this.height = height;
        this.hitBoxOnly = isHitbox;
    }
    public override float GetPointDepth(Vector2 point)
    {
        throw new System.NotImplementedException();//ne doper cho tyt 
    }

    public Tuple<Vector2, Vector2> GetSideLine(Side side) {
        Vector2 from;
        Vector2 to;

        switch (side) {
            case Side.right:
                from = new Vector2(width * 0.5f, height * 0.5f);
                to = new Vector2(width * 0.5f, -height * 0.5f);
                break;
            case Side.top:
                from = new Vector2(width * 0.5f, height * 0.5f);
                to = new Vector2(-width * 0.5f, height * 0.5f);
                break;
            case Side.left:
                from = new Vector2(-width * 0.5f, -height * 0.5f);
                to = new Vector2(-width * 0.5f, height * 0.5f);
                break;
            default:
                from = new Vector2(-width * 0.5f, -height * 0.5f);
                to = new Vector2(width * 0.5f, -height * 0.5f);
                break;
        }

        if (angle == 0.0f) {
            return new Tuple<Vector2, Vector2>(from, to);
        }

        float fromLen = from.magnitude;
        float fromAngle = Mathf.Atan2(from.x, from.y);

        float toLen = to.magnitude;
        to = to.normalized;
        float toAngle = Mathf.Atan2(to.x, to.y);

        float radAngle = angle * Mathf.Deg2Rad;

        float xFrom = Mathf.Cos(fromAngle + radAngle);
        float yFrom = Mathf.Sin(fromAngle + radAngle);

        from = new Vector2(xFrom, yFrom) * fromLen + position;
        
        float xTo = Mathf.Cos(toAngle + radAngle);
        float yTo = Mathf.Sin(toAngle + radAngle);

        to = new Vector2(xTo, yTo) * toLen + position;

        return new Tuple<Vector2, Vector2>(from, to);
    }
}
