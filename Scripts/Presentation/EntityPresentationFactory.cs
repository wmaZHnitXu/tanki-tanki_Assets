using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityPresentationFactory : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected Queue<EntityPresentation> pooled;

    public EntityPresentation AllocatePresentation(Entity target) {
        EntityPresentation result;
        if (pooled.Count == 0) {
            result = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<EntityPresentation>();
        }
        else {
            result = pooled.Dequeue();
        }

        result.SetTargetAndUpdate(target);

        return result;
    }

    public void PutInPool(EntityPresentation presentation) {
        pooled.Enqueue(presentation);
    }

    public abstract Type GetEntityType();
}
