using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollideableEntity : Entity
{
    protected Collider _collider;
    public Collider collider {
        get => _collider;
        set {
            _collider = value;
        }
    }
}
