using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float _thickness;
    private float thickness {
        get => _thickness;
        set {
            if (_thickness == value) return;

            _thickness = Mathf.Clamp(value, 0.0f, 1.0f);
            mainBar.color = new Color(mainBar.color.r, mainBar.color.g, mainBar.color.b, thickness * baseAlphaMain);
            additionalBar.color = new Color(additionalBar.color.r, additionalBar.color.g, additionalBar.color.b, thickness * baseAlphaAddit);
            backGround.color = new Color(backGround.color.r, backGround.color.g, backGround.color.b, thickness * baseAlphaBG);
        }
    }

    [SerializeField] private Image mainBar;
    [SerializeField] private Image additionalBar;
    [SerializeField] private Image backGround;
    [SerializeField] private float baseAlphaBG = 1.0f;
    [SerializeField] private float baseAlphaMain = 1.0f;
    [SerializeField] private float baseAlphaAddit = 1.0f;
    [SerializeField] private float disapSpeed;
    [SerializeField] private Vector3 offset;

    public DestructiblePresentation presentation;
    private float _hpOld;
    private float hpOld {
        get => _hpOld;
        set {
            if (_hpOld == value) return;

            var ent = presentation.destructible;
            _hpOld = value;
            if (presentation.isActiveAndEnabled) {
                additionalBar.rectTransform.SetRight(GetRightForHp(ent.maxHealth, value));
            }
            else {
                additionalBar.rectTransform.SetRight(GetRightForHp(1.0f, 0.0f));
                thickness = 0.0f;
            }
        }
    }
    private float _hpActual;
    private float hpActual {
        get => _hpActual;
        set {
            if (_hpActual == value) return;

            var ent = presentation.destructible;
            _hpActual = value;
            mainBar.rectTransform.SetRight(GetRightForHp(ent.maxHealth, value));
        }
    }

    private float GetRightForHp(float maxHealth, float health) {
        float width = backGround.rectTransform.rect.width;
        return width * (1f - health / maxHealth);
    }

    void Update() {
        if (HealthBarsController.instance == null || presentation == null) return;
        thickness -= disapSpeed;
        transform.position = presentation.transform.position + offset;
        if (Mathf.Abs(hpOld - hpActual) < 0.001f) {
            hpOld = hpActual;
        }
        else {
            hpOld += (hpActual - hpOld) * 0.01f;
        }
        if (thickness <= 0.0f) {
            Detach();
        }
    }

    public void ConnectToPresentation(DestructiblePresentation presentation, float initDamage) {
        this.presentation = presentation;
        hpActual = presentation.destructible.health;
        hpOld = presentation.destructible.health + initDamage;
    }

    private void Detach() {
        HealthBarsController.instance.ReturnMeToPool(this);
        presentation = null;
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
        hpActual = presentation.destructible.health;
        thickness = 1.0f;
    }

    void OnValidate() {
        if (mainBar == null || additionalBar == null || backGround == null) return;
        else {
            baseAlphaAddit = additionalBar.color.a;
            baseAlphaBG = backGround.color.a;
            baseAlphaMain = mainBar.color.a;
        }
    }
}
