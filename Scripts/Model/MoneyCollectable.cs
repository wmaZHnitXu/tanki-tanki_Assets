using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollectable : CollectableEntity
{
    
    public int nominal; 
    
    public MoneyCollectable(Level level, int nominal, Vector2 position, Vector2 velocity) : base(level, position, velocity) {
        this.nominal = nominal;
    }

    protected override void Collect()
    {
        Kill();
    }
}
