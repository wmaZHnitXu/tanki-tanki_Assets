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
    [SerializeField] private bool notSupposedToHaveImageComponent;


    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 originalScale;
    public AnimationTW appearAnim;
    public AnimationTW disappearAnim;
    public bool transformAllOfTheChildren;
    private AnimationTW currentAnimation;
    private float duration;
    private bool inPlay;
    [SerializeField] private float timestamp;
    [SerializeField] private bool playAtStart;
    private float startTime;
    [SerializeField] private Image myImageComponent;

    [System.Serializable]
    public class AnimationTW {
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
        InvertQuad,
        InvertCube
    }

    public List<TwinzelTween> children = new List<TwinzelTween>();

    public delegate float EasingFunc(float x);

    public EasingFunc GetEasingFunc(EasingFuncType type) {
        switch(type) {
            case EasingFuncType.Quad: return easingQuad;
            case EasingFuncType.InvertQuad: return easingInvertQuad;
            case EasingFuncType.InvertCube: return easingInvertCube;
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

    private float easingInvertCube(float x) {
        return -Math.Abs((x-1) * (x-1) * (x-1)) + 1;
    }

    [SerializeField] private List<TwinzelTween> childTweeners = new List<TwinzelTween>();
    [ContextMenu("Add tweeners from children gameobjects")]
    public void AddTweenersFromChildren() {
        childTweeners = new List<TwinzelTween>();

        foreach (TwinzelTween twin in transform.GetComponentsInChildren<TwinzelTween>(true)) {
            if (twin == this) continue;
            childTweeners.Add(twin);
        }
    }

    void Start() {
        if (playAtStart) {
            PlayAnimation(appearAnim);
        }
    }

    public void SetPosition(float x, AnimationTW anim) {
        Vector3 pos = InterpVector(anim.startPos, anim.endPos, x);
        pos = new Vector3(pos.x * Screen.width, pos.y * Screen.height, pos.z);
        transform.localPosition = originalPosition + pos;
    }

    public void SetColor(float x, AnimationTW anim) {
        if (myImageComponent != null) {
            Color color = InterpColor(anim.startColor, anim.endColor, x);
            myImageComponent.color = color;
        }
    }

    public void SetScale(float x, AnimationTW anim) {
        float scaleFactor = InterpFloat(anim.startScale, anim.endScale, x);
        Debug.Log(originalScale * scaleFactor + " " + scaleFactor + " " + originalScale);
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
        gameObject.SetActive(true);
        inPlay = true;
    }

    public void PlayAnimation(AnimationTW anim, float duration) {
        currentAnimation = anim;
        this.duration = duration;
    }

    public void Appear() {
        PlayAnimation(appearAnim);
        foreach (TwinzelTween twin in children) {
            twin.Appear();
        }
    }

    public void Disappear() {
        PlayAnimation(disappearAnim);
        foreach (TwinzelTween twin in children) {
            twin.Disappear();
        }
    }

    void Update() {
        if (inPlay) {
            timestamp = (Time.time - startTime) / duration;
            if (timestamp >= 1.0f) {
                timestamp = 1.0f;
                inPlay = false;
                if (currentAnimation == disappearAnim) {
                    gameObject.SetActive(false);
                }
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

    [ContextMenu("Validate")]
    void Val() {
        myImageComponent = GetComponent<Image>();
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
    }

    PreviewType prevAnimationForTimestampReset;
    void OnValidate() {
        if (prevAnimationForTimestampReset != previewState) {
            preview = 0f;
            prevAnimationForTimestampReset = previewState;
        } 
        
        if (myImageComponent != null || notSupposedToHaveImageComponent) {
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
