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
        //Application.targetFrameRate = 60;
        FindObjectOfType<PresentationManager>().Initialize();
        FindObjectOfType<UIManager>().Initialize();
        FindObjectOfType<HealthBarsController>().Initialize();
        FindObjectOfType<ParticleSystems>().Initialize();
        controller = GetComponent<PlayerControllerPC>();

        UIManager.instance.activeOverlay = UIManager.MainOverlayType.Greeting;
        //level.DoUpdate(0.1f);
    }

    int i = 0;
    void Update() {
        if (level != null) {
            //if (i++ % 100 == 0)
                level.DoUpdate(Time.deltaTime * 1.0f);

            for (int i = 0; i < 4; i++) {
                RectCollider rect = playerTank.colliders[1] as RectCollider;
                var tuple = rect.GetSideLine((RectCollider.Side)i);

                Vector2 lineStart = tuple.Item1;
                Vector2 lineEnd = tuple.Item2;

                var v1 = new Vector3(lineStart.x, lineStart.y, 0.1f);
                var v2 = new Vector3(lineEnd.x, lineEnd.y, 0.1f);

                var v21 = rect.position;
                var v22 = rect.GetNormal(transform.position) + v21;

                Debug.DrawLine(v1, v2, Color.red, 1.0f, true);
                Debug.DrawLine(v21, v22, Color.red, 0.05f, true);
            }
        }
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
        playerTank = null;
    }

    public void LoadLevel(int levelNum, out Tank plrtnk) {
        level = new Level(32, 32);

        plrtnk = new Tank(level, new TankInfo(), controller);
        level.SetPlayerTank(plrtnk);

        OnLevelLoadEvent?.Invoke(level);

        new Tile(level, new Vector2(2f, 2f));
        new Tile(level, new Vector2(1f, 1f));
        new Tile(level, new Vector2(1f, 2f));
        new Tile(level, new Vector2(1f, 3f));
        new Tile(level, new Vector2(1f, 5f));
        new Tile(level, new Vector2(1f, 6f));
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                new Tile(level, new Vector2(i - 15, j));
            }
        }
        
        var tank = new Tank(level, new TankInfo(), null);
    }
}
