using System.Collections.Generic;

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

    List<Entity> entities;

    public Level(int width, int height) {
        this.width = width;
        this.height = height;
        tiles = new int[width, height];
        entities = new List<Entity>();
    }

    public void DoUpdate(float delta) {
        foreach (Entity entity in entities) {
            entity.Update(this, delta);
        }
    }

    public void AddEntity(Entity entity) {
        entities.Add(entity);
    }
}
