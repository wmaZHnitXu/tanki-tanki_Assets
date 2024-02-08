using UnityEngine;

public class Overlay : MonoBehaviour
{
    [SerializeField] protected TwinzelTween tweener;
    private bool _isActive;
    public bool isActive {
        get => _isActive;
        set => _isActive = value;
    }

    public virtual void Appear() {
        tweener.Appear();
        isActive = true;
    }

    public virtual void Disappear() {
        tweener.Disappear();
        isActive = false;
    }
}
