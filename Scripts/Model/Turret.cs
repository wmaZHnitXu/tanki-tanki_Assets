using System;
using UnityEngine;
using static TankInfo;

public abstract class Turret : AttachedEntity
{
    public readonly TurretType type;
    public readonly int turretLevel;
    public float yaw {
        get => rotation;
        set => rotation = value;
    }

    public Vector2 velocity {
        get => ownerTank.velocity;
    }

    private bool _isFiring;
    public bool isFiring {
        get => _isFiring;
        set => _isFiring = value;
    }

    protected Tank ownerTank;

    public delegate void OnReloading();
    public event OnReloading OnReloadingEvent;
    

    public Turret(Level level, Tank owner, TankInfo info) : base(level, owner) {
        this.type = info.turretType;
        this.turretLevel = info.turretLevel;
        this.ownerTank = owner as Tank;
    }

    public static Turret Create(Level level, Tank owner, TankInfo info) {
        if (false)
        return new ShotgunTurret(level, owner, info);
        switch (info.turretType) {
            case TurretType.shilka:
                return new ShilkaTurret(level, owner, info);
            case TurretType.shotgun:
                return new ShotgunTurret(level, owner, info);
            default:
                throw new NotImplementedException();
        }
    }
}
