using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityPresentation : MonoBehaviour
{
    private Entity _target;
    public Entity target {
        get => _target;
        protected set => _target = value;
    }
    public GameObject prefab;
    public delegate void OnDisposed(EntityPresentation presentation);
    public event OnDisposed OnDisposedEvent;
    public virtual void SetTargetAndUpdate(Entity target) {
        gameObject.SetActive(true);
        this.target = target;
        target.OnDeathEvent += Dispose;
        transform.position = target.position;
    }

    public virtual void Dispose(Entity ent) {
        gameObject.SetActive(false);
        target.OnDeathEvent -= Dispose;
        target = null;
        OnDisposedEvent?.Invoke(this);
    }
}
