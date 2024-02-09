using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityPresentationFactory : MonoBehaviour
{
    public abstract EntityPresentation AllocatePresentation(Entity target, GameObject prefab);
}
