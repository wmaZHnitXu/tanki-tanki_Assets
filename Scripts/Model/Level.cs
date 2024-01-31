using System.Collections.Generic;
using System.Numerics;

public class Level
{
    private int _width;
    private int _height;
    public int width {
        get => _width;
        set {
            _width = value;
        }
    }
    public int height {
        get => _height;
        set {
            _height = value;
        }
    }

    int[,] tiles;

    public HashSet<Entity> entities;

    List<Entity> toRemove = new List<Entity>();
    List<Entity> toAdd = new List<Entity>();

    public delegate void OnEntityAdded(Entity entity);
    public event OnEntityAdded OnEntityAddedEvent;

    public Level(int width, int height) {
        this.width = width;
        this.height = height;
        tiles = new int[width, height];
        entities = new HashSet<Entity>();
    }

    public void DoUpdate(float delta) {
        foreach (Entity entity in entities) {
            entity.Update(this, delta);
        }

        RemoveRemovedEntities();
        AddAddedEntities();
    }

    public void AddEntity(Entity entity) {
        toAdd.Add(entity);
    }

    public bool TraceLine(Entity ignored, Vector2 from, Vector2 to, ref Vector2 hitPos, ref Entity entity) {
        return true;
    }

    private void AddAddedEntities() {
        foreach (Entity entity in toAdd) {
            if (entities.Add(entity)) {
                entity.OnDeathEvent += RemoveEntity;
                OnEntityAddedEvent(entity);
            }
        }
        toAdd.Clear();
    }

    private void RemoveRemovedEntities() {
        foreach (Entity entity in toRemove) {
            entities.Remove(entity);
        }
        toRemove.Clear();
    }

    public void RemoveEntity(Entity entity) {
        toRemove.Add(entity);
    }
}
