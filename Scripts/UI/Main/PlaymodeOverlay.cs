using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaymodeOverlay : MainOverlay
{
    [SerializeField] private Image healthActual;
    [SerializeField] private Image healthOld;
    [SerializeField] private float healthOldFollowRate;

    public void InsertPlayer(Tank playerTank) {

    }

    void Update() {
        var playerTank = Game.instance.playerTank;
        if (playerTank != null) {
            float newHealth = playerTank.health;
            float maxHealth = playerTank.maxHealth;

            float ratio = newHealth / maxHealth;

            if (healthActual.fillAmount != ratio) {
                healthActual.fillAmount = ratio;
            }

            float o = healthOld.fillAmount;
            float a = healthActual.fillAmount;
            if (o != a) {
                healthOld.fillAmount += (a - o) * healthOldFollowRate;
                if (Mathf.Abs(o - a) < 0.01f) {
                    healthOld.fillAmount = a;
                }
            }
        }
    }


    public void B_Pause() {
        Game.instance.ExitLevel();
        UIManager.instance.activeOverlay = UIManager.MainOverlayType.Garage;
    }
    
}
