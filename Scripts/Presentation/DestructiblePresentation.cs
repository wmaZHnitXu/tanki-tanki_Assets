using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructiblePresentation : EntityPresentation
{
    protected float healthBarCd;
    protected float maxHealthBarCd;

    public delegate void ShowHp(float startHp, float endHp);
    public event ShowHp ShowHpEvent;
    private DestructibleEntity _destructible;
    public DestructibleEntity destructible {
        get => _destructible;
        protected set => _destructible = value;
    }

    public override void SetTargetAndUpdate(Entity target) {
        if (destructible != null) {
            throw new System.Exception();
        }
        base.SetTargetAndUpdate(target);
        destructible = target as DestructibleEntity;
        destructible.OnDamageEvent += OnDamage;
        destructible.OnDestructionEvent += OnDeath;
    }

    public virtual void OnDamage(DestructibleEntity destructible, float amount, Vector2 from) {
        if (destructible == null) {
            Debug.Log(target == null);
        }
        
        HealthBarsController.instance.HighlightHealth(this, amount);   
    }

    public virtual void OnDeath(Entity destructible) {
        
    }

    public override void Dispose(Entity ent)
    {
        base.Dispose(ent);
        this.destructible.OnDestructionEvent -= OnDeath;
        this.destructible.OnDamageEvent -= OnDamage;
        destructible = null;
    }
}
