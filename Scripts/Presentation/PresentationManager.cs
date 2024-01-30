using System;
using System.Collections.Generic;
using UnityEngine;

public class PresentationManager : MonoBehaviour
{
    public static PresentationManager instance;
    private Dictionary<Type, EntityPresentationFactory> entityFactories;

    public void Initialize() {
        instance = this;

        entityFactories = new Dictionary<Type, EntityPresentationFactory>();

        var entFactoryComponents = gameObject.GetComponents<EntityPresentationFactory>();
        foreach (EntityPresentationFactory factory in entFactoryComponents) {
            entityFactories.Add(factory.GetEntityType(), factory);
        }
    }

    
}
