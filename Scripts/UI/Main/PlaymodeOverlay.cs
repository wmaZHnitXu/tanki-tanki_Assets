using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaymodeOverlay : MainOverlay
{

    public void B_Pause() {
        Game.instance.ExitLevel();
        UIManager.instance.activeOverlay = UIManager.MainOverlayType.Garage;
    }
    
}
