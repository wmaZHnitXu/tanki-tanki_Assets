using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuOverlay : MainOverlay
{
    public void B_GoInside() {
        UIManager.instance.activeOverlay = UIManager.MainOverlayType.Garage;
    }
    
}
