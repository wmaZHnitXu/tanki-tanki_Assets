using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public enum MainOverlayType : int {
        Greeting,
        Garage,
        LvlSelect,
        OnLevel,
        LevelEnd
    }

    public enum SmallOverlayType : int {
        Settings,
        Pause
    }

    [SerializeField] private SmallOverlayType _smallActiveOverlay;
    public SmallOverlayType activeSmallOverlay {
        get => _smallActiveOverlay;
        set {
            GetSmallOverlay(_smallActiveOverlay).Disappear();
            _smallActiveOverlay = value;
            GetSmallOverlay(_smallActiveOverlay).Appear();
        }
    }

    [SerializeField] private MainOverlayType _activeOverlay;
    public MainOverlayType activeOverlay {
        get => _activeOverlay;
        set {
            GetMainOverlay(_activeOverlay).Disappear();
            _activeOverlay = value;
            GetMainOverlay(_activeOverlay).Appear();
        }
    } 



    [SerializeField] private List<MainOverlay> mainOverlays;
    [SerializeField] private List<SmallOverlay> smallOverlays;
    public MainOverlay GetMainOverlay(MainOverlayType type) {
        return mainOverlays[(int)type];
    }
    public SmallOverlay GetSmallOverlay(SmallOverlayType type) {
        return smallOverlays[(int)type];
    }

    public void Initialize() {
        instance = this;
    }

    public bool settingStatus;
}
