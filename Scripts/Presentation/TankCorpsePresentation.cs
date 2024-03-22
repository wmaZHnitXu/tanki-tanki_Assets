using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCorpsePresentation : EntityPresentation
{
    public SpriteRenderer bodySprite;
    public SpriteRenderer turretSprite;
    public Transform turretTransform;
    public TankCorpse corpse;
    private float lastRot;
    private Vector2 lastPos;

    public override void SetTargetAndUpdate(Entity target) {
        base.SetTargetAndUpdate(target);
        corpse = target as TankCorpse;

        bodySprite.sprite = TankSpritesHolder.instance.GetHullSprite(corpse.tankInfo);
        turretSprite.sprite = TankSpritesHolder.instance.GetTurretSprite(corpse.tankInfo);

        transform.position = target.position;
        lastPos = target.position;
        lastRot = target.rotation;
        transform.rotation = Quaternion.Euler(0, 0, lastRot);
        turretTransform.rotation = Quaternion.Euler(0, 0, corpse.turretRotation);

    }

    protected override void Update() {
        if (lastPos != corpse.position || lastRot != corpse.rotation) {
            lastPos = corpse.position;
            lastRot = corpse.rotation;
            transform.position = lastPos;
            transform.rotation = Quaternion.Euler(0, 0, lastRot);
        }
    }
    
}
