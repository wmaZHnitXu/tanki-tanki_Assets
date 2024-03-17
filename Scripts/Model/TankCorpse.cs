using UnityEngine;

public class TankCorpse : DestructibleEntity, IPushable
{
    public readonly TankInfo tankInfo;
    public TankCorpse(Level level, TankInfo tankInfo, Vector2 position, Vector2 velocity) : base(level)
    {
        this.position = position;
        this.velocity = velocity;
        this.tankInfo = tankInfo;
    }

    public Vector2 velocity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public bool CanPush(IPushable pushable)
    {
        throw new System.NotImplementedException();
    }

    public override float GetOuterRadius()
    {
        throw new System.NotImplementedException();
    }

    public override bool MustBeDestroyedForLevelToEnd()
    {
        throw new System.NotImplementedException();
    }
}
