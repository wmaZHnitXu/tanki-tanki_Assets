using System;
using static TankInfo;

public abstract class Turret : AttachedEntity
{
    public readonly TurretType type;
    public readonly int level;
    public float yaw {
        get => rotation;
        set => rotation = value;
    }

    public delegate void OnReloading();
    public event OnReloading OnReloadingEvent;
    

    public Turret(Tank owner, TankInfo info) : base(owner) {
        this.type = info.turretType;
        this.level = info.turretLevel;
    }

    public abstract int GetAmmoCount();
    public abstract int GetMaxAmmo();

    public static Turret Create(Tank owner, TankInfo info) {
        switch (info.turretType) {
            case TurretType.shilka:
                return new ShilkaTurret(owner, info);
            default:
                throw new NotImplementedException();
        }
    }
}
