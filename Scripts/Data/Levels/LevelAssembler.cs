


using UnityEngine;

public abstract class LevelAssembler : MonoBehaviour
{
  public abstract Level AssembleLevel(int levelNum, out Tank playerTank, TankInfo playerTankInfo, PlayerController controller);
}
