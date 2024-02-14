using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : DestructibleEntity
{
    public Tile(Level level, Vector2 position) : base(level) {
        colliders = new List<Collider>
        {
            new RectCollider(this, 1f, 1f)
        };
        this.position = position;

        this.maxHealth = 200.0f;
        this.health = this.maxHealth;
    }

    protected override void Death() {

    }

    public override bool MustBeDestroyedForLevelToEnd()
    {
        return false;
    }
}
