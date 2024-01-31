using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityPresentation : MonoBehaviour
{
    protected Entity target;
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
        OnDisposedEvent?.Invoke(this);
    }
}
