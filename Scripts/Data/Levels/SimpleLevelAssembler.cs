

using UnityEngine;

public class SimpleLevelAssembler : LevelAssembler
{
    public override Level AssembleLevel(int levelNum, out Tank playerTank, TankInfo playerTankInfo, PlayerController controller)
    {
        var level = new Level();
        UIManager.instance.OnReturnedToGarage += Game.instance.ExitLevel;
        
        Tank plrtnk = new Tank(level, playerTankInfo, Vector2.zero, controller);

        if (levelNum != 2) {
            new TileBlock(level, new Vector2(2f, 2f));
            new TileBlock(level, new Vector2(1f, 1f));
            new TileBlock(level, new Vector2(1f, 2f));
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    new TileBlock(level, new Vector2(i - 15, j));
                }
            }
        }
        new TileBlock(level, new Vector2(1f, 3f));
        new TileBlock(level, new Vector2(1f, 5f));
        new TileBlock(level, new Vector2(1f, 6f));
        
        
        var tank = new Tank(level, new TankInfo(), Vector2.zero, new CirclingDummyController());
        //new Tank(level, new TankInfo(), Vector2.zero, new CirclingDummyController());
        //new Tank(level, new TankInfo(), Vector2.zero, new CirclingDummyController());
        //new Tank(level, new TankInfo(), Vector2.zero, new CirclingDummyController());
        new Tank(level, new TankInfo(), Vector2.zero, null);
        
        playerTank = plrtnk;
        return level; 
    }
}
