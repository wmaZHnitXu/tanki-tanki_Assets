using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum MainOverlayType : int {
        Greeting,
        Garage,
        LvlSelect,
        OnLevel
    }

    [SerializeField] private MainOverlayType _activeOverlay;
    public MainOverlayType activeOverlay {
        get => _activeOverlay;
        set {
            GetOverlayTweener(_activeOverlay).Disappear();
            _activeOverlay = value;
            GetOverlayTweener(_activeOverlay).Appear();
        }
    }


    [SerializeField] private List<TwinzelTween> overlayTweeners;
    public TwinzelTween GetOverlayTweener(MainOverlayType type) {
        return overlayTweeners[(int)type];
    }

    public void ButtonSetOverlay(int overlayType) {
        activeOverlay = (MainOverlayType)overlayType;
    }

    public bool settingStatus;
}
