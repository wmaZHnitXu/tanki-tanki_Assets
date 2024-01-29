using UnityEngine;

public class Tank : DestructibleEntity
{
    private Turret head;
    public float speed;
    public float acceleration;
    public Vector2 velocity;
    public Vector2 lookPos;    
    public override void Update(Level level, float delta) {

    }

    public override void Damage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Damage(float damage, Vector2 hitPos)
    {
        throw new System.NotImplementedException();
    }
}
