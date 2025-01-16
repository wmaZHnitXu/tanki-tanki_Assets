
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public delegate void OnLevelLoad(Level level);
    public event OnLevelLoad OnLevelLoadEvent;
    public void Initialize() {
        instance = this;
    }
    public void LoadLevel(int levelNum, out Tank plrtnk) {
        SimpleLevelFactory factory = new();

        ILevel level = factory.CreateLevel();

        plrtnk = level.Load();

        OnLevelLoadEvent.Invoke(Game.instance.level);
    }
}
