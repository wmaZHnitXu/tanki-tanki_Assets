using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CollectableEntity : CollideableEntity, IPushable
{
    protected Vector2 _velocity;
    public Vector2 velocity { get => _velocity; set => _velocity = value; }

    protected bool isFollowing;
    protected float followingDistance;
    protected float collectDistance;
    protected float followSpeed;

    public virtual bool CanPush(IPushable pushable)
    {
        return false;
    }

    public override void Update(float delta)
    {
        position += velocity;
        velocity *= 0.01f;

        var plr = level.playerTank;
        if (plr == null) return;
        
        float distance = Vector2.Distance(plr.position, position);
        if (distance < followingDistance) {
            isFollowing = true;
        }

        if (isFollowing) {
            velocity = (plr.position - position) * followSpeed;
            if (distance < collectDistance) {
                Collect();
            }
        }
    }

    protected virtual void Collect() {
        Kill();
    }

    public CollectableEntity(Level level, Vector2 position, Vector2 velocity) : base(level) {
        this.position = position;
        this.velocity = velocity;

        this.collectDistance = 0.1f;
        this.followingDistance = 1.0f;
        this.followSpeed = 0.05f;
    }
}
