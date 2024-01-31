using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPresentation : EntityPresentation
{

    void Update() {
        transform.position = target.position;
    }
    
}
