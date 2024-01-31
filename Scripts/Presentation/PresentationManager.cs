using System;
using System.Collections.Generic;
using UnityEngine;

public class PresentationManager : MonoBehaviour
{
    public static PresentationManager instance;
    public List<EntityPresentationFactory> factoryList = new List<EntityPresentationFactory>();
    private Dictionary<Type, EntityPresentationFactory> entityFactories;

    public void Initialize() {
        instance = this;

        factoryList.Add(gameObject.GetComponent<TankFactoryPresentation>());

        entityFactories = new Dictionary<Type, EntityPresentationFactory>();

        var entFactoryComponents = gameObject.GetComponents<EntityPresentationFactory>();
        foreach (EntityPresentationFactory factory in entFactoryComponents) {
            entityFactories.Add(factory.GetEntityType(), factory);
        }

        Game.instance.OnLevelLoadEvent += SetLevelListening;
    }

    private void AllocatePresentation(Entity entity) {
        var factory = entityFactories.GetValueOrDefault(entity.GetType());
        factory.AllocatePresentation(entity);
    }

    private void SetLevelListening(Level level) {
        level.OnEntityAddedEvent += AllocatePresentation;
    }
}
