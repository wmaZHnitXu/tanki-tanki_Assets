using UnityEngine;

public class TankInfo
{
    public enum TurretType : int {
        shilka,
        shotgun,
        ricochet,
        flamethrower,
        cannon,
        tesla,
        rocket,
        rail,
        laser
        
    }

    public TurretType turretType;

    public int turretLevel;

    public enum HullType : int {
        light,
        medium,
        heavy
    }
    
    public HullType hullType;
    public int hullLevel;

    public TankInfo() {
        this.turretType = TurretType.cannon;
        this.hullType = HullType.heavy;
        this.turretLevel = 1;
        this.hullLevel = 1;
    }

    public float GetHullWidth() {
        switch (hullType) {
            case HullType.light: return 0.75f;
            case HullType.medium: return 0.875f;
            case HullType.heavy: return 1.0f;
            default: return 0.875f; 
        }
    }
}
