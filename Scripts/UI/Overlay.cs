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
        tweener.OnAppearEvent += OnAppeared;
    }

    public virtual void Disappear() {
        tweener.Disappear();
        isActive = false;
        tweener.OnDisappearEvent += OnDisappeared;
    }

    public virtual void OnDisappeared() {
        tweener.OnDisappearEvent -= OnDisappeared;
        OnDisappearEvent?.Invoke();
    }

    public virtual void OnAppeared() {
        tweener.OnAppearEvent -= OnAppeared;
        OnAppearEvent?.Invoke();
    }
}
