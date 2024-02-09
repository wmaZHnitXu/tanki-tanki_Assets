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
        this.turretType = TurretType.shilka;
        this.hullType = HullType.medium;
        this.turretLevel = 1;
        this.hullLevel = 1;
    }
}
