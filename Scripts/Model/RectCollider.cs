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

        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);

        from = new Vector2(from.x * cos + from.y * sin, from.y * cos + from.x * sin);
        to = new Vector2(to.x * cos + to.y * sin, to.y * cos + to.x * sin);

        from += position;
        to += position;

        return new Tuple<Vector2, Vector2>(from, to);
    }
}
