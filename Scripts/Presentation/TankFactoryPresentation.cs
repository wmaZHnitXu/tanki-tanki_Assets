using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFactoryPresentation : EntityPresentationFactory
{
    public override Type GetEntityType()
    {
        return typeof(Tank);
    }
}
