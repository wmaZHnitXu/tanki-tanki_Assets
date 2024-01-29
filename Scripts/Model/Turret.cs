public class Turret
{
    private float _yaw;
    public float yaw {
        get => _yaw;
        set {
            _yaw = value;
        }
    }
    public Tank owner;
}
