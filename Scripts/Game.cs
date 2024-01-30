using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    public enum GameState {
        Garage,
        OnLevel
    }
    public GameState state;
    private Level level;
    public Tank playerTank;
    void Awake()
    {
        instance = this;
        LoadLevel(0);
    }

    void Update() {
        level.DoUpdate(Time.deltaTime * 1.0f);
    }

    public void LoadLevel(int levelNum) {
        state = GameState.OnLevel;

        level = new Level(32, 32);
        playerTank = new Tank(new Turret(), GetComponent<PlayerControllerPC>());
        level.AddEntity(playerTank);
    }
}
