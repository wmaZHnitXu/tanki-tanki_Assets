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
        for (int i = 0; i < 10; i++) {
            var rand = (Vector2)Random.insideUnitSphere;
            Vector2 randomisedPos = position + rand * 0.5f;
            
            new MoneyCollectable(level, Random.Range(0, 11), randomisedPos, Vector2.zero);
        }

        foreach (Entity entity in level.entities) {
            if (entity is Tank) {
                (entity as Tank).Damage(Random.Range(10f, 15f));
            }
        }
    }

    public override bool MustBeDestroyedForLevelToEnd()
    {
        return false;
    }

    public override float GetOuterRadius() {
        return 1.4142f;
    }
}
