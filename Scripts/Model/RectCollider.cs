using UnityEngine;

public class RectCollider : Collider
{
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
}
