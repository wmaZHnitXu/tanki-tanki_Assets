using UnityEngine;

public abstract class Entity
{
    private Vector2 _position;
    public Vector2 position {
        get => _position;
        private set {
            _position = value;
        }
    }

    private float _rotation;
    public float rotation {
        get => _rotation;
        private set {
            _rotation = value;
        }
    }

    public virtual void Update(Level level, float delta) {

    }

    public virtual void Kill() {
        
    }
}
