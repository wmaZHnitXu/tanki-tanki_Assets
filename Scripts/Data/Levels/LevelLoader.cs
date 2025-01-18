
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public delegate void OnLevelLoad(Level level);
    public event OnLevelLoad OnLevelLoadEvent;
    [SerializeField] private List<LevelAssembler> levelAssemblerEntires;
    public void Initialize() {
        instance = this;
    }
    public Level LoadLevel(int levelNum, out Tank playerTank, TankInfo playerTankInfo, PlayerController controller) {
        Tank resultPlayerTank;
        Level level = levelAssemblerEntires[levelNum].AssembleLevel(levelNum,out resultPlayerTank, playerTankInfo, controller);
        playerTank = resultPlayerTank;
        level.SetPlayerTank(playerTank);
        OnLevelLoadEvent.Invoke(level);
        return level;
    }
}
