using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public List<Entity> entities;
    public CollideableEntity[] collideables = new CollideableEntity[0];
    private int _profit;
    public int profit {
        get => _profit;
        set {
            _profit = value;
            OnProfitChangedEvent?.Invoke(this, profit);
        }
    }

    List<Entity> toRemove = new List<Entity>();
    List<Entity> toAdd = new List<Entity>();

    public delegate void OnEntityAdded(Entity entity);
    public event OnEntityAdded OnEntityAddedEvent;

    public delegate void OnLevelEnded(Level level);
    public event OnLevelEnded OnLevelEndedEvent;

    public delegate void OnProfitChanged(Level level, int profit);
    public event OnProfitChanged OnProfitChangedEvent;
    public bool isComplete;
    private Tank _playerTank;
    public Tank playerTank {
        get => _playerTank;
        private set => _playerTank = value;
    }

    public void SetPlayerTank(Tank playerTank) {
        this.playerTank = playerTank;
    }

    public Level(int width, int height) {
        this.width = width;
        this.height = height;
        tiles = new int[width, height];
        entities = new List<Entity>();
    }

    public void DoUpdate(float delta) {
        foreach (Entity entity in entities) {
            entity.Update(delta);
        }

        RemoveRemovedEntities();
        AddAddedEntities();

        SolveCollisions();
    }

    private void SolveCollisions() {
        //foreach (CollideableEntity pushCandidate in collideables) {
        var x = collideables.Length;
        for (int i = 0; i < x; i++) {
            var pushCandidate = collideables[i];
            if (pushCandidate is IPushable) {
                //foreach (CollideableEntity collidant in collideables) {
                    for (int j = 0; j < x; j++) {
                    var collidant = collideables[j];

                    //CHECK
                    if (collidant == pushCandidate) continue;
                    var pushable = pushCandidate as IPushable;
                    if (collidant is IPushable) {
                        if (!(collidant as IPushable).CanPush(pushable)) {
                            continue;
                        }
                    }

                    
                    float sqrR1 = pushCandidate.GetOuterRadius();

                    float sqrR2 = collidant.GetOuterRadius();
                    
                    if (Vector2.Distance(collidant.position, pushCandidate.position) > sqrR1 + sqrR2) {
                        continue;
                    }
                    

                    //PROCEED
                    var vec = Physics.GetSeparationVector(pushable, collidant);
                    pushable.position += vec;
                    
                    if (collidant is not IPushable)
                        pushable.velocity += vec * 1f;
                    else {
                        (collidant as IPushable).velocity -= vec * 10f;
                    }
                    
                }
            }
        }
    }

    public void AddEntity(Entity entity) {
        toAdd.Add(entity);
    }

    public Entity TraceLine(Predicate<CollideableEntity> canGoThrough, Vector2 from, Vector2 to, out Vector2 hitPos) {
        return Physics.TraceLine(collideables, canGoThrough, from, to, out hitPos);
    }

    public Entity TraceLine(Predicate<CollideableEntity> canGoThrough, Vector2 from, Vector2 to, out Vector2 hitPos, out Vector2 normal) {
        return Physics.TraceLine(collideables, canGoThrough, from, to, out hitPos, out normal);
    }
    public List<Entity> GetEntitiesForSplash(Vector2 hitPos, float splashRange, Entity ignor)
    {
        return Physics.GetEntitiesForSplash(collideables, hitPos, splashRange, ignor);
    }

    private void AddAddedEntities() {
        int collideableCnt = 0;
        foreach (Entity entity in toAdd) {
            entities.Add(entity);
            entity.OnDeathEvent += RemoveEntity;
            OnEntityAddedEvent(entity);

            if (entity is CollideableEntity) {
                collideableCnt++;
            }
        }

        int c = 0;
        if (collideableCnt != 0) {
            CollideableEntity[] newArr = new CollideableEntity[collideables.Length + collideableCnt];
            for (int i = 0; i < collideables.Length; i++) {
                newArr[i] = collideables[i];
            }
            foreach (Entity entity in toAdd) {
                if (entity is CollideableEntity)
                    newArr[collideables.Length + c++] = entity as CollideableEntity;
            }
            collideables = newArr;
        }
        toAdd.Clear();
    }

    private void RemoveRemovedEntities() {

        int collideableCnt = 0;
        bool removedSomething = false;

        foreach (Entity entity in toRemove) {
            if (entities.Remove(entity)) {
                removedSomething = true;
            }

            if (entity is CollideableEntity) {
                collideableCnt++;
            }
        }

        int c = 0;
        if (collideableCnt != 0) {
            CollideableEntity[] newArr = new CollideableEntity[collideables.Length - collideableCnt];
            foreach (CollideableEntity entity in collideables) {
                if (!toRemove.Contains(entity)) {
                    newArr[c++] = entity;
                }
            }
            collideables = newArr;
        }

        toRemove.Clear();

        if (removedSomething && CheckLevelComplete() && !isComplete) {
            Debug.Log("Level complete!");
            isComplete = true;
            CompleteLevel();
        }
    }

    public void RemoveEntity(Entity entity) {
        toRemove.Add(entity);
    }

    public bool CheckLevelComplete() {
        foreach (Entity entity in collideables) {
            if (entity is DestructibleEntity) {
                if ((entity as DestructibleEntity).MustBeDestroyedForLevelToEnd()) return false;
            }
        }
        return true;
    }

    public void CompleteLevel() {
        OnLevelEndedEvent?.Invoke(this);
    }

    public void Destroy() {
        foreach (Entity entity in entities) {
            entity.Kill(true);
        }
    }
}
