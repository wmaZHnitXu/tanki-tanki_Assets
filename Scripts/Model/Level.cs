using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

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
    public HashSet<CollideableEntity> collideables = new HashSet<CollideableEntity>();

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

    private void SolveCollisions() {
        foreach (CollideableEntity pushCandidate in collideables) {
            if (pushCandidate is IPushable) {
                foreach (CollideableEntity collidant in collideables) {
                    if (collidant == pushCandidate) continue;
                }
            }
        }
    }

    private Vector2 GetSeparationVector(IPushable pushable, CollideableEntity collidant) {
        /*
        TODO: bolee izyawnoe rewenie
        Poka wto soidet, pywto y nas bol'we tipov collaiderov i ne planiryeca
        Da ny i ewe po yrodski viglyadit no vi4islyaeca ne medlennee ne-yrodskogo varianta
        */
        Vector2 result = Vector2.zero;
        float x = 0, y = 0;
        Vector2 GetVectorForColliders (Collider pushableCol, Collider collidantCol) {
            bool flag = pushableCol is CircleCollider;
            var c1 = pushableCol;
            var c2 = collidantCol;
            if (c1 is CircleCollider) {
                var circle1 = c1 as CircleCollider;
                if (c2 is CircleCollider) {
                    var circle2 = c2 as CircleCollider;
                    float depth = circle1.GetPointDepth(circle2.position);
                    depth += circle2.radius;
                    result = (circle1.position - circle2.position) * depth;
                }
                else if (c2 is RectCollider) {
                    var rect2 = c2 as RectCollider;

                }
                else {
                    return Vector2.zero;
                }
            }
            else if (c1 is RectCollider) {
                
            }
        }
        return result;
    }

    public void AddEntity(Entity entity) {
        toAdd.Add(entity);
    }

    public Entity TraceLine(Entity ignored, Vector2 from, Vector2 to, out Vector2 hitPos) {
        float dis = Vector2.Distance(from, to);
        float counter = 0;
        Entity result = null;
        Vector2 resHitPos = Vector2.zero;

        for (;counter < dis; counter++)
        {
            float x = from.x + counter * Mathf.Cos(lookAngle * Mathf.Deg2Rad);
            float y = from.y + counter * Mathf.Sin(lookAngle * Mathf.Deg2Rad);

            foreach (CollideableEntity entity in collideables)
            {
                if (entity.position.x == x || entity.position.y == y)
                {
                    resHitPos = new Vector2(x, y);
                    result = entity;
          
                }
                else
                {
                    resHitPos = Vector2.zero;
                    result =  null;
                }
            }
        }
        hitPos = resHitPos;
        return result;
    }

    private void AddAddedEntities() {
        foreach (Entity entity in toAdd) {
            if (entities.Add(entity)) {
                entity.OnDeathEvent += RemoveEntity;
                OnEntityAddedEvent(entity);

                if (entity is CollideableEntity) {
                    collideables.Add(entity as CollideableEntity);
                }
            
            }
        }
        toAdd.Clear();
    }

    private void RemoveRemovedEntities() {
        foreach (Entity entity in toRemove) {
            entities.Remove(entity);

            if (entity is CollideableEntity) {
                collideables.Remove(entity as CollideableEntity);
            }
        }
        toRemove.Clear();
    }

    public void RemoveEntity(Entity entity) {
        toRemove.Add(entity);
    }
}
