
using UnityEngine;

public class SimpleLevel : Level, ILevel
{
    public SimpleLevel(int width, int height) : base(width, height)
    {
    }

    public Tank Load(){
        UIManager.instance.OnReturnedToGarage += Game.instance.ExitLevel;
        
        Tank plrtnk = new Tank(Game.instance.level, Game.instance.playerInfo.selectedTank, Vector2.zero, Game.instance.controller);

        new TileBlock(Game.instance.level, new Vector2(2f, 2f));
        new TileBlock(Game.instance.level, new Vector2(1f, 1f));
        new TileBlock(Game.instance.level, new Vector2(1f, 2f));
        new TileBlock(Game.instance.level, new Vector2(1f, 3f));
        new TileBlock(Game.instance.level, new Vector2(1f, 5f));
        new TileBlock(Game.instance.level, new Vector2(1f, 6f));
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                new TileBlock(Game.instance.level, new Vector2(i - 15, j));
            }
        }
        
        var tank = new Tank(Game.instance.level, new TankInfo(), Vector2.zero, new CirclingDummyController());
        //new Tank(Game.instance.level, new TankInfo(), Vector2.zero, new CirclingDummyController());
        //new Tank(level, new TankInfo(), Vector2.zero, new CirclingDummyController());
        //new Tank(level, new TankInfo(), Vector2.zero, new CirclingDummyController());
        new Tank(Game.instance.level, new TankInfo(), Vector2.zero, null);

        return plrtnk; 
    }
}
