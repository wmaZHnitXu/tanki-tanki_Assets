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
    public PlayerController controller;
    public delegate void OnLevelLoad(Level level);
    public event OnLevelLoad OnLevelLoadEvent;
    void Start()
    {
        instance = this;
        GetComponent<PresentationManager>().Initialize();
        controller = GetComponent<PlayerControllerPC>();
        //level.DoUpdate(0.1f);
    }

    void Update() {
        if (state == GameState.OnLevel)
            level.DoUpdate(Time.deltaTime * 1.0f);
        //Debug.Log(level.entities.Count);
    }

    public void SelectLevel(int levelNum) {
        if (level != null) {
            level.Destroy();
        }
        LoadLevel(levelNum, out playerTank);
        GetComponent<CameraMovement>().Initialize(controller, playerTank);
        state = GameState.OnLevel;
    }

    public void LoadLevel(int levelNum, out Tank plrtnk) {
        state = GameState.OnLevel;

        level = new Level(32, 32);
        OnLevelLoadEvent?.Invoke(level);

        plrtnk = new Tank(new Turret(), controller);
        level.AddEntity(playerTank);

        level.AddEntity(new Tile(new Vector2(2f, 2f)));
        level.AddEntity(new Tile(new Vector2(1f, 1f)));
        level.AddEntity(new Tile(new Vector2(1f, 2f)));
        level.AddEntity(new Tile(new Vector2(1f, 3f)));
        level.AddEntity(new Tile(new Vector2(1f, 5f)));
        level.AddEntity(new Tile(new Vector2(1f, 6f)));

        var tank = new Tank(new Turret(), null);
        level.AddEntity(tank);
    }
}
