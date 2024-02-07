public class PlayerInfo
{
    public TankInfo selectedTank;
    public int level;
    public int money;

    public PlayerInfo() {
        this.selectedTank = new TankInfo();
        this.money = 10000;
        this.level = 0;
    }
}
