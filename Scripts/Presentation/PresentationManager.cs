using System;
using System.Collections.Generic;
using UnityEngine;

public class PresentationManager : MonoBehaviour
{
    public static PresentationManager instance;
    [SerializeField] private List<EntityFactoryEntry> entityEntries;

    [Serializable]
    public struct EntityFactoryEntry {
        public string typeName;
        public GameObject prefab;
        public EntityPresentationFactory factory;
    }

    public void Initialize() {
        instance = this;
        LevelLoader.instance.OnLevelLoadEvent += SetLevelListening;
        FindObjectOfType<TankSpritesHolder>().Initialize();
    }

    private void AllocatePresentation(Entity entity) {
        EntityFactoryEntry correspondingEntry = entityEntries[0];
        foreach (EntityFactoryEntry entry in entityEntries) {
            if (entry.typeName == entity.GetType().ToString()) {
                correspondingEntry = entry;
            }
        }
        //Debug.Log(correspondingEntry.typeName);
        //Debug.Log(correspondingEntry.factory == null);
        //Debug.Log(correspondingEntry.prefab == null);
        
        correspondingEntry.factory.AllocatePresentation(entity, correspondingEntry.prefab);
    }

    private void SetLevelListening(Level level) {
        level.OnEntityAddedEvent += AllocatePresentation;
    }
}
