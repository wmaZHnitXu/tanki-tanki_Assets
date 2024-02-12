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

    private bool _isFiring;
    public bool isFiring {
        get => _isFiring;
        set => _isFiring = value;
    }

    public delegate void OnReloading();
    public event OnReloading OnReloadingEvent;
    

    public Turret(Tank owner, TankInfo info) : base(owner) {
        this.type = info.turretType;
        this.level = info.turretLevel;
    }

    public static Turret Create(Tank owner, TankInfo info) {
        switch (info.turretType) {
            case TurretType.shilka:
                return new ShilkaTurret(owner, info);
            case TurretType.shotgun:
                return new ShotgunTurret(owner, info);
            default:
                throw new NotImplementedException();
        }
    }
}
