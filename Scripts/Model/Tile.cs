using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : DestructibleEntity
{
    public Tile(Vector2 position) {
        colliders = new List<Collider>
        {
            new RectCollider(this, 1f, 1f)
        };
        this.position = position;
    }
    public override void Damage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Damage(float damage, Vector2 hitPos)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(Level level, float delta)
    {

    }
}
