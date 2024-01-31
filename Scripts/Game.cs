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
    public delegate void OnLevelLoad(Level level);
    public event OnLevelLoad OnLevelLoadEvent;
    void Awake()
    {
        instance = this;
        GetComponent<PresentationManager>().Initialize();
        LoadLevel(0);
    }

    void Update() {
        level.DoUpdate(Time.deltaTime * 1.0f);
        //Debug.Log(level.entities.Count);
    }

    public void LoadLevel(int levelNum) {
        state = GameState.OnLevel;

        level = new Level(32, 32);
        OnLevelLoadEvent?.Invoke(level);

        playerTank = new Tank(new Turret(), GetComponent<PlayerControllerPC>());
        level.AddEntity(playerTank);
    }
}
