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

        this.maxHealth = 100.0f;
        this.health = this.maxHealth;
    }

    public override void Update(Level level, float delta)
    {

    }
}
