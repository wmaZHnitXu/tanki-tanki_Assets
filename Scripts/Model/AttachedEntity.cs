using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttachedEntity : Entity
{
    public readonly Entity owner;
    public override Vector2 position { 
        get => owner.position; 
        set => owner.position = value;
    }
    public AttachedEntity(Entity owner) {
        this.owner = owner;
        owner.OnDeathEvent += OwnerDeath;
    }
    public virtual void OwnerDeath(Entity placehold) {
        Kill();
    }
}
