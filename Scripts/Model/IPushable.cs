using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public interface IPushable
{
    void AddVelocity(Vector2 velocity);
    public List<Collider> colliders {
        get;
    }

    public Vector2 position {
        get;
    }
}
