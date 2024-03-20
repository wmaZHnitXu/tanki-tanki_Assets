using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpritesHolder : MonoBehaviour
{
    public List<HullSprites> hulls;
    public List<TurretSprites> turrets;
    public Sprite GetHullSprite(TankInfo info) {
        return GetHullSprite(info.hullType, info.hullLevel);
    }

    public Sprite GetHullSprite(TankInfo.HullType type, int level) {
        return hulls[(int)type].hullLevels[level];
    }

    public Sprite GetTurretSprite(TankInfo info) {
        return GetTurretSprite(info.turretType, info.turretLevel);
    }

    public Sprite GetTurretSprite(TankInfo.TurretType type, int level) {
        return turrets[(int)type].turretLevels[level];
    }

    [System.Serializable]
    public class HullSprites {
        public List<Sprite> hullLevels;
        public List<Sprite> hullLevelsEmissions;
    }

    public class TurretSprites {
        public List<Sprite> turretLevels;

    }
}
