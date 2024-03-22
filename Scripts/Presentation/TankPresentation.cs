using UnityEngine;

public class TankPresentation : DestructiblePresentation
{
    [SerializeField] float camSpeed;
    [SerializeField] Transform camTrans;
    [SerializeField] Transform body;
    [SerializeField] SpriteRenderer hullSprite;
    [SerializeField] SpriteRenderer emtSprite;
    private Tank tank;
    Level level;

    public override void SetTargetAndUpdate(Entity target) {
        base.SetTargetAndUpdate(target);
        tank = target as Tank;

        hullSprite.sprite = TankSpritesHolder.instance.GetHullSprite(tank.info);
    }

    protected override void Update()
    {
        transform.position = tank.position;
        body.localRotation = Quaternion.Euler(0,0, tank.rotation);

        if (camTrans == null) return;
        Vector3 camTarget = new Vector3(transform.position.x, transform.position.y, camTrans.position.z);
        camTrans.position = Vector3.Lerp(camTrans.position, camTarget, camSpeed * Time.deltaTime);
    }

    public override void OnDeath(Entity entity) {
        ParticleSystems.instance.PlayOnPos(transform.position, ParticleSystems.ParticleType.TankDebris);
    }
}
