using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollectablePresentation : EntityPresentation
{
    protected MoneyCollectable money;
    private float angle;
    private bool initialized = false;
    Vector3 initScale;
    [SerializeField] SpriteRenderer spriteRenderer;
    public override void SetTargetAndUpdate(Entity target)
    {
        base.SetTargetAndUpdate(target);
        money = target as MoneyCollectable;

        if (!initialized) {
            initScale = transform.localScale;
            var ic = spriteRenderer.color;
            float br = Random.Range(0.7f, 1.2f);
            spriteRenderer.color = new Color(ic.r * br, ic.g * br, ic.b * br, ic.a);
            initialized = true;
        }
        float multipler = money.nominal * 0.3f + 1.0f;
        angle = Random.Range(0.0f, 360.0f);
        transform.localScale = initScale * multipler;


    }

    protected override void Update()
    {
        if (money.isFollowing) {
            base.Update();
        }
    }
}
