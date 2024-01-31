using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactoryPresentation : EntityPresentationFactory
{
    public override Type GetEntityType()
    {
        return typeof(Bullet);
    }
}
