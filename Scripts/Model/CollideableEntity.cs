using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollideableEntity : Entity
{
    private List<Collider> _colliders;

    protected CollideableEntity(Level level) : base(level) {}


    public List<Collider> colliders {
        get => _colliders;
        protected set => _colliders = value;
    }
}
