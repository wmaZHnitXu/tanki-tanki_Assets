using System;
using UnityEngine;
using static RectCollider;

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

    public override Vector2 GetNormal(Vector2 point)
    {
        if(width != height)
        {
            throw new NotImplementedException();
        }

        Vector2 p = MathUtil.GlobalToLocalCoords(point, this);

        var tuple1 = GetSideLine(Side.top);
        var tuple2 = GetSideLine(Side.bottom);

        Vector2 p1 = tuple1.Item1;
        Vector2 p2 = tuple1.Item2;
        Vector2 p3 = tuple2.Item2;
        Vector2 p4 = tuple2.Item1;

        float d1 = Vector2.Distance(p, p1);
        float d2 = Vector2.Distance(p, p2);
        float d3 = Vector2.Distance(p, p3);
        float d4 = Vector2.Distance(p, p4);

        float[] dss ={ d1+d2, d2+d3, d3+d4, d4+d1 };

        float d = dss[0];

        for (int i = 1; i < dss.Length; i++) {
            if(d < dss[i]) d = dss[i];
        }

        Vector2 result = Vector2.zero;

        if (d == dss[1]) //r
        {
            //result = MathUtil.LocalToGlobalCoords(new Vector2(1, 0), this);
            result = new Vector2(1, 0);
        }
        if (d == dss[0]) //t
        {
            //result = MathUtil.LocalToGlobalCoords(new Vector2(0, 1), this);
            result = new Vector2(0, 1);
        }
        if (d == dss[3]) //l
        {
            //result = MathUtil.LocalToGlobalCoords(new Vector2(-1, 0), this);
            result = new Vector2(-1, 0);
        }
        if (d == dss[2]) //b
        {
            //result = MathUtil.LocalToGlobalCoords(new Vector2(0, -1), this);
            result = new Vector2(0, -1);
        }

        return result;
    }
}
