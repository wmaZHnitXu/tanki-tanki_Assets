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
        FindObjectOfType<PresentationManager>().Initialize();
        FindObjectOfType<UIManager>().Initialize();
        controller = GetComponent<PlayerControllerPC>();

        UIManager.instance.activeOverlay = UIManager.MainOverlayType.Greeting;
        //level.DoUpdate(0.1f);
    }

    void Update() {
        if (level != null)
            level.DoUpdate(Time.deltaTime * 1.0f);
        //Debug.Log(level.entities.Count);
    }

    public void SelectLevel(int levelNum) {
        if (level != null) {
            level.Destroy();
        }
        LoadLevel(levelNum, out playerTank);
        FindObjectOfType<CameraMovement>().Initialize(controller, playerTank);
    }

    public void ExitLevel() {
        level.Destroy();
        level = null;
    }

    public void LoadLevel(int levelNum, out Tank plrtnk) {
        level = new Level(32, 32);
        OnLevelLoadEvent?.Invoke(level);

        plrtnk = new Tank(new TankInfo(), controller);
        level.AddEntity(playerTank);

        level.AddEntity(new Tile(new Vector2(2f, 2f)));
        level.AddEntity(new Tile(new Vector2(1f, 1f)));
        level.AddEntity(new Tile(new Vector2(1f, 2f)));
        level.AddEntity(new Tile(new Vector2(1f, 3f)));
        level.AddEntity(new Tile(new Vector2(1f, 5f)));
        level.AddEntity(new Tile(new Vector2(1f, 6f)));

        var tank = new Tank(new TankInfo(), null);
        level.AddEntity(tank);
    }
}
