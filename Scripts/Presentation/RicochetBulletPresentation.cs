using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBulletPresentation : EntityPresentation
{
    [SerializeField] private Vector2 vel;
    [SerializeField] private Vector2 sex;

    void Update() {
        transform.position = target.position;
        vel = (target as RicochetBullet).velocity;
        sex = (target as RicochetBullet).sex;
    }
    
}
