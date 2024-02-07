using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectOverlay : MainOverlay
{

    public void B_ToLevel(int id) {
        Game.instance.SelectLevel(id);
        UIManager.instance.activeOverlay = UIManager.MainOverlayType.OnLevel;
    }

    public void B_BackToGarage() {
        UIManager.instance.activeOverlay = UIManager.MainOverlayType.Garage;
    }
    
}
