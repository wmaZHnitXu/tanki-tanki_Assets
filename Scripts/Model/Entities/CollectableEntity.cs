using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CollectableEntity : Entity
{
    protected Vector2 _velocity;
    public Vector2 velocity { get => _velocity; set => _velocity = value; }

    public bool isFollowing;
    protected float followingDistance;
    protected float collectDistance;
    protected float followSpeed;

    public CollectableEntity(Level level, Vector2 position, Vector2 velocity) : base(level) {
        this.position = position;
        this.velocity = velocity;

        this.collectDistance = 0.3f;
        this.followingDistance = 2.0f;
        this.followSpeed = 600f;
    }

    public virtual bool CanPush(IPushable pushable)
    {
        return false;
    }

    public override void Update(float delta)
    {

        var plr = level.playerTank;
        if (plr == null) return;
        //Debug.Log("followa");
        
        float distance = Vector2.Distance(plr.position, position);
        if (distance < followingDistance && !isFollowing) {
            isFollowing = true;
        }

        if (isFollowing) {
            velocity = ((plr.position - position) * followSpeed * delta / (distance * distance));
            position += velocity * delta;
            if (distance < collectDistance) {
                Collect();
            }
        }
    }

    protected virtual void Collect() {
        Kill();
    }
}
