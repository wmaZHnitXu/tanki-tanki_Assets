using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBulletPresentation : EntityPresentation
{
    void Update() {
        transform.position = target.position;
    }
    
}
