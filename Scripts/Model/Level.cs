using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level
{
    private int _width;
    private int _height;
    public int width {
        get => _width;
        set => _width = value;
    }
    public int height {
        get => _height;
        set => _height = value;
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

        SolveCollisions();
    }

    private void SolveCollisions() {
        foreach (CollideableEntity pushCandidate in collideables) {
            if (pushCandidate is IPushable) {
                foreach (CollideableEntity collidant in collideables) {
                    if (collidant == pushCandidate) continue;
                    var pushable = pushCandidate as IPushable;
                    var vec = GetSeparationVector(pushable, collidant);
                    pushable.position += vec;
                    
                    if (collidant is not IPushable)
                        pushable.velocity += vec * 50f;
                    else {
                        (collidant as IPushable).velocity -= vec * 10f;
                    }
                    
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

        Vector2 GetVectorForColliders (Collider pushableCol, Collider collidantCol) {
            
            if (pushableCol.hitBoxOnly || collidantCol.hitBoxOnly) return Vector2.zero;

            Vector2 vector = Vector2.zero;
            bool flag = pushableCol is CircleCollider;
            var c1 = pushableCol;
            var c2 = collidantCol;
            if (c1 is CircleCollider) {
                var circle1 = c1 as CircleCollider;
                if (c2 is CircleCollider) {
                    var circle2 = c2 as CircleCollider;
                    float depth = circle1.GetPointDepth(circle2.position);
                    depth += circle2.radius;
                    vector = (circle1.position - circle2.position) * Mathf.Max(depth, 0.0f);
                }
                else if (c2 is RectCollider) {
                    var rect2 = c2 as RectCollider;
                    Vector2 edgePoint = circle1.position - rect2.position;
                    edgePoint = new Vector2(
                        Mathf.Clamp(edgePoint.x, -rect2.width * 0.5f, rect2.width * 0.5f),
                        Mathf.Clamp(edgePoint.y, -rect2.height * 0.5f, rect2.height * 0.5f))
                         + rect2.position;
                    if (Math.Abs(edgePoint.x) != rect2.width  * 0.5f && Math.Abs(edgePoint.y) != rect2.height  * 0.5f) {
                        edgePoint += (rect2.position - edgePoint) * 0.01f;
                    }
                    Vector2 expulsionVector = (circle1.position - edgePoint);
                    expulsionVector = expulsionVector.magnitude > 0.0f ? expulsionVector.normalized : new Vector2(rect2.width * 0.5f, 0.0f);
                    float depth = circle1.GetPointDepth(edgePoint);
                    if (depth > 0.0f) {
                        vector = expulsionVector * depth;
                    }
                    else {
                        vector = Vector2.zero;
                    }
                }
                else {
                    //TODO
                    return Vector2.zero;
                }
            }
            else if (c1 is RectCollider) {

            }
            return vector;
        }

        Vector2 bestVector = Vector2.zero;
        foreach (Collider col1 in pushable.colliders) {
            foreach (Collider col2 in collidant.colliders) {
                Vector2 separ = GetVectorForColliders(col1, col2);
                if (bestVector.magnitude < separ.magnitude) {
                    bestVector = separ;
                }
            }
        }

        return bestVector;
    }

    public void AddEntity(Entity entity) {
        toAdd.Add(entity);
    }

    public Entity TraceLine(Entity ignored, Vector2 from, Vector2 to, out Vector2 hitPos) {
        float k;
        float b;
        float d = Vector2.Distance(from, to);

        MathUtil.GetCoefficientsForLine(from, to, out k, out b);
        if (from.x == to.x) k = float.MaxValue;

        Entity result = null;
        hitPos = to;

        foreach (CollideableEntity entity in collideables) {

            if (entity == ignored) continue;

            foreach(Collider col in entity.colliders)
            {
                if (col is CircleCollider)
                {
                    var circle = col as CircleCollider;
                    Vector2 v1;
                    Vector2 v2;


                    MathUtil.SolveItersectionOfCircleAndLine(k, b, circle.position.x, circle.position.y, circle.radius, out v1, out v2);

                    //Debug.Log("v1 " + v1);
                    //Debug.Log("v2 " + v2);

                    float d1 = Vector2.Distance(from, v1);
                    float d2 = Vector2.Distance(from, v2);

                    if (d >= d1 || d >= d2) {
                        //Debug.Log(true);
                        result = entity;    
                        hitPos = d1 < d2 ? v1 : v2;
                        d = d1 < d2 ? d1 : d2;
                    }
                }
                else if (col is RectCollider) {
                    var rect = col as RectCollider;
                    for (int i = 0; i < 4; i++) {
                        var side = (RectCollider.Side)i;

                        var tuple = rect.GetSideLine(side);

                        Vector2 lineStart = tuple.Item1;
                        Vector2 lineEnd = tuple.Item2;

                        float k1, b1;
                        MathUtil.GetCoefficientsForLine(lineStart, lineEnd, out k1, out b1);
                        Vector2 sideIntersection;
                        if (lineStart.x == lineEnd.x) {
                            float x = lineStart.x;
                            float y = k * x + b;
                            sideIntersection = new Vector2(x, y);
                        }
                        else {
                            sideIntersection = MathUtil.GetLineIntersection(k, b, k1, b1);
                        }

                        float len1, d0, d1, len2, d2, d3;

                        len1 = Vector2.Distance(from, to);
                        d0 = Vector2.Distance(from, sideIntersection);
                        d1 = Vector2.Distance(to, sideIntersection);

                        len2 = Vector2.Distance(lineStart, lineEnd);
                        d2 = Vector2.Distance(lineStart, sideIntersection);
                        d3 = Vector2.Distance(lineEnd, sideIntersection);

                        if ((d0 < len1 && d1 < len1) && (d2 < len2 && d3 < len2) && d0 < d) {
                            result = entity;    
                            hitPos = sideIntersection;
                            d = d0;
                        }
                    }
                }
            }
          
        }

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

    public void Destroy() {
        foreach (Entity entity in entities) {
            entity.Kill(true);
        }
    }
}
