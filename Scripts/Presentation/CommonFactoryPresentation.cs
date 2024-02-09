using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFactoryPresentation : EntityPresentationFactory
{
    protected Dictionary<GameObject, Queue<EntityPresentation>> objectPools = new Dictionary<GameObject, Queue<EntityPresentation>>();
    int c = 0;

    public override EntityPresentation AllocatePresentation(Entity target, GameObject prefab)
    {
        EntityPresentation result;

        //Vinimaem Queue that corresponds to this prefab or sozdaem novyjy
        Queue<EntityPresentation> pooled = null;

        if (objectPools.ContainsKey(prefab)) {
            pooled = objectPools[prefab];
        }
        else {
            var queueForPrefab = new Queue<EntityPresentation>();
            objectPools.Add(prefab, queueForPrefab);
            pooled = queueForPrefab;
        }


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
        result.prefab = prefab;
        result.OnDisposedEvent += PutInPool;

        return result;
    }

    private void PutInPool(EntityPresentation presentation) {
        
        var pooled = objectPools[presentation.prefab];
        presentation.OnDisposedEvent -= PutInPool;
        //Destroy(presentation.gameObject);
        pooled.Enqueue(presentation);
    }

}
