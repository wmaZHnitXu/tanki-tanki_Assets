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

    public HashSet<Entity> entities;
    public HashSet<CollideableEntity> collideables = new HashSet<CollideableEntity>();

    List<Entity> toRemove = new List<Entity>();
    List<Entity> toAdd = new List<Entity>();

    public delegate void OnEntityAdded(Entity entity);
    public event OnEntityAdded OnEntityAddedEvent;

    public delegate void OnLevelEnded(Level level);
    public event OnLevelEnded OnLevelEndedEvent;
    private Tank _playerTank;
    public Tank playerTank {
        get => _playerTank;
        private set => _playerTank = value;
    }

    public void SetPlayerTank(Entity entity) {
        playerTank = playerTank;
    }

    public Level(int width, int height) {
        this.width = width;
        this.height = height;
        tiles = new int[width, height];
        entities = new HashSet<Entity>();
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
        foreach (CollideableEntity pushCandidate in collideables) {
            if (pushCandidate is IPushable) {
                foreach (CollideableEntity collidant in collideables) {

                    //CHECK
                    if (collidant == pushCandidate) continue;
                    var pushable = pushCandidate as IPushable;
                    if (collidant is IPushable) {
                        if (!(collidant as IPushable).CanPush(pushable)) {
                            continue;
                        }
                    }

                    //PROCEED
                    var vec = Physics.GetSeparationVector(pushable, collidant);
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

    public void AddEntity(Entity entity) {
        toAdd.Add(entity);
    }

    public Entity TraceLine(Predicate<CollideableEntity> canGoThrough, Vector2 from, Vector2 to, out Vector2 hitPos) {
        return Physics.TraceLine(collideables, canGoThrough, from, to, out hitPos);
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

        if (CheckLevelEnded()) {
            EndLevel();
        }
    }

    public void RemoveEntity(Entity entity) {
        toRemove.Add(entity);
    }

    public bool CheckLevelEnded() {
        foreach (Entity entity in collideables) {
            if (entity is DestructibleEntity) {
                if ((entity as DestructibleEntity).MustBeDestroyedForLevelToEnd()) return false;
            }
        }
        return true;
    }

    public void EndLevel() {
        OnLevelEndedEvent?.Invoke(this);
    }

    public void Destroy() {
        foreach (Entity entity in entities) {
            entity.Kill(true);
        }
    }
}
