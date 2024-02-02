using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwinzelTween : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float preview;
    public enum PreviewType {
        None,
        Appear,
        Disappear
    }
    [SerializeField] private PreviewType previewState;


    private Vector3 originalPosition;
    private Vector3 originalScale;
    public AnimationTW appearAnim;
    public AnimationTW disappearAnim;
    public bool transformAllOfTheChildren;
    private AnimationTW currentAnimation;
    private float duration;
    private bool inPlay;
    [SerializeField] private float timestamp;
    private float startTime;
    [SerializeField] private Image myImageComponent;

    [System.Serializable]
    public struct AnimationTW {
        public Vector3 startPos;
        public Vector3 endPos;
        public float startScale;
        public float endScale;
        public Color startColor;
        public Color endColor;
        public float defaultDuration;
        public EasingFuncType easing;
        public bool coolEffect1;
        public bool coolEffect2;
        public bool coolEffect3;
    }

    public enum EasingFuncType {
        Linear,
        Quad,
        InvertQuad
    }

    public delegate float EasingFunc(float x);

    public EasingFunc GetEasingFunc(EasingFuncType type) {
        switch(type) {
            case EasingFuncType.Quad: return easingQuad;
            case EasingFuncType.InvertQuad: return easingInvertQuad;
            default: return easingFuncLinear;
        }
    }

    private float easingFuncLinear(float x) {
        return x;
    }

    private float easingQuad(float x) {
        return x * x;
    }

    private float easingInvertQuad(float x) {
        return -((x-1) * (x-1)) + 1;
    }

    [SerializeField] private List<TwinzelTween> childrenTweeners = new List<TwinzelTween>();

    void Start() {
        originalPosition = transform.position;
        originalScale = transform.localScale;
        if (myImageComponent == null) {
            myImageComponent = GetComponent<Image>();
        }

        PlayAnimation(appearAnim);
    }

    public void SetPosition(float x, AnimationTW anim) {
        Vector3 pos = InterpVector(anim.startPos, anim.endPos, x);
        pos = new Vector3(pos.x * Screen.width, pos.y * Screen.height, pos.z);
        transform.position = originalPosition + pos;
    }

    public void SetColor(float x, AnimationTW anim) {
        Color color = InterpColor(anim.startColor, anim.endColor, x);
        myImageComponent.color = color;
    }

    public void SetScale(float x, AnimationTW anim) {
        float scaleFactor = InterpFloat(anim.startScale, anim.endScale, x);
        transform.localScale = originalScale * scaleFactor;
    }

    public void SetAnimTimestamp(float t) {
        float x = GetEasingFunc(currentAnimation.easing).Invoke(t);
        SetPosition(x, currentAnimation);
        SetColor(x, currentAnimation);
        SetScale(x, currentAnimation);
    }

    public void PlayAnimation(AnimationTW anim) {
        startTime = Time.time;
        currentAnimation = anim;
        duration = anim.defaultDuration;
        inPlay = true;
    }

    public void PlayAnimation(AnimationTW anim, float duration) {
        currentAnimation = anim;
        this.duration = duration;
    }

    void Update() {
        if (inPlay) {
            timestamp = (Time.time - startTime) / duration;
            if (timestamp >= 1.0f) {
                timestamp = 1.0f;
                inPlay = false;
            }
            SetAnimTimestamp(timestamp);
        }
    }

    public Color InterpColor(Color a, Color b, float value) {
        return new Color(
        a.r + value * (b.r - a.r),
        a.g + value * (b.g - a.g),
        a.b + value * (b.b - a.b),
        a.a + value * (b.a - a.a));
    }

    public Vector3 InterpVector(Vector3 a, Vector3 b, float value) {
        return new Vector3(
        a.x + value * (b.x - a.x),
        a.y + value * (b.y - a.y),
        a.z + value * (b.z - a.z));

    }

    public float InterpFloat(float a, float b, float value) {
        return a + value * (b - a);
    }

    void OnValidate() {

        if (myImageComponent == null && previewState == PreviewType.None) {
            myImageComponent = GetComponent<Image>();
            originalPosition = transform.position;
            originalScale = transform.localScale;
        }
        else {
            if (previewState == PreviewType.Appear) {
                currentAnimation = appearAnim;
                duration = 1.0f;
                SetAnimTimestamp(preview);
            }
            if (previewState == PreviewType.Disappear) {
                currentAnimation = disappearAnim;
                duration = 1.0f;
                SetAnimTimestamp(preview);
            }
            
        }
    }

}
