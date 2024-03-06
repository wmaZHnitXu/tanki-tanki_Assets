using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;

    public void Initialize() {
        instance = this;
    }

    public PlayerInfo GetPlayerInfoSync() {
        return new PlayerInfo();
    }
    
}
