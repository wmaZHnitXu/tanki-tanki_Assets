using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageOverlay : MainOverlay
{

    public void B_ToLevelSelect() {
        UIManager.instance.activeOverlay = UIManager.MainOverlayType.LvlSelect;
    }

    public void B_BackToMenu() {
        UIManager.instance.activeOverlay = UIManager.MainOverlayType.Greeting;
    }
    
}
