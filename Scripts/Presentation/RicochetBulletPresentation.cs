using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBulletPresentation : EntityPresentation
{
    protected override void Update() {
        transform.position = target.position;
    }
    
}
