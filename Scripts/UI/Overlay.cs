using UnityEngine;
using UnityEngine.Events;

public class Overlay : MonoBehaviour
{
    public TwinzelTween tweener;
    private bool _isActive;
    public bool isActive {
        get => _isActive;
        set => _isActive = value;
    }

    public UnityAction OnAppearEvent;
    public UnityAction OnDisappearEvent;

    public virtual void Appear() {
        tweener.Appear();
        isActive = true;
        tweener.OnAppearEvent += OnAppear;
    }

    public virtual void Disappear() {
        tweener.Disappear();
        isActive = false;
        tweener.OnDisappearEvent += OnDisappear;
    }

    public virtual void OnDisappear() {
        tweener.OnDisappearEvent -= OnDisappear;
        OnDisappearEvent?.Invoke();
    }

    public virtual void OnAppear() {
        tweener.OnAppearEvent -= OnAppear;
        OnAppearEvent?.Invoke();
    }
}
