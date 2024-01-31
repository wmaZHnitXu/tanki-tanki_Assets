using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityPresentationFactory : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected Queue<EntityPresentation> pooled = new Queue<EntityPresentation>();
    int c;

    public EntityPresentation AllocatePresentation(Entity target) {
        EntityPresentation result;
        if (pooled.Count == 0) {
            var tankPres = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<EntityPresentation>();
            tankPres.gameObject.name += ++c;
            result = tankPres as EntityPresentation;
        }
        else {
            result = pooled.Dequeue();
            //Debug.Log("Deq_" + result.gameObject.name);
        }

        result.SetTargetAndUpdate(target);
        result.OnDisposedEvent += PutInPool;

        return result;
    }

    public void PutInPool(EntityPresentation presentation) {
        //Debug.Log("Enq_" + presentation.gameObject.name);
        presentation.OnDisposedEvent -= PutInPool;
        //Destroy(presentation.gameObject);
        pooled.Enqueue(presentation);
    }

    public abstract Type GetEntityType();
}
