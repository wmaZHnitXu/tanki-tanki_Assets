using System.Collections.Generic;
using UnityEngine;

public class TankCorpse : DestructibleEntity, IPushable
{
    public readonly TankInfo tankInfo;
    private float stoppingRate = 0.1f;
    private Vector2 _velocity; 
    public TankCorpse(Level level, TankInfo tankInfo, Vector2 position, Vector2 velocity) : base(level)
    {
        this.position = position;
        this.velocity = velocity;
        this.tankInfo = tankInfo;

        colliders = new List<Collider>(){
            new CircleCollider(this, tankInfo.GetHullWidth() * 0.5f),
            new RectCollider(this, tankInfo.GetHullWidth(), tankInfo.GetHullWidth(), true)
        };
    }

    public Vector2 velocity { get => _velocity; set => _velocity = value; }

    public bool CanPush(IPushable pushable)
    {
        return true;
    }

    public override float GetOuterRadius()
    {
        return 0.5f;
    }

    public override void Update(float delta)
    {
        velocity -= velocity / (delta * stoppingRate);
    }

    public override bool MustBeDestroyedForLevelToEnd()
    {
        return false;
    }
}
