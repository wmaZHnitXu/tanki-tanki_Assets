using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityPresentationFactory : MonoBehaviour
{
    [SerializeField] protected Queue<EntityPresentation> pooled = new Queue<EntityPresentation>();
    public abstract EntityPresentation AllocatePresentation(Entity target, GameObject prefab);

    public void PutInPool(EntityPresentation presentation) {
        //Debug.Log("Enq_" + presentation.gameObject.name);
        presentation.OnDisposedEvent -= PutInPool;
        //Destroy(presentation.gameObject);
        pooled.Enqueue(presentation);
    }
}
