using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public interface IPushable
{
    public List<Collider> colliders {
        get;
    }

    public Vector2 position {
        get; set;
    }

    public Vector2 velocity {
        get; set;
    }
}
