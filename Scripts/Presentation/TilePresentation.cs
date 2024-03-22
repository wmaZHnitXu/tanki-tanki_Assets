using UnityEngine;

public class TilePresentation : DestructiblePresentation
{
    public override void OnDeath(Entity destructible)
    {
        base.OnDeath(destructible);
        ParticleSystems.instance.PlayOnPos(destructible.position, ParticleSystems.ParticleType.Crate);
        CameraMovement.instance.Shake(((Vector2)Camera.main.transform.position - destructible.position).normalized);
    }
}
