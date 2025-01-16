

public class SimpleLevelFactory : SuperLevelFactory
{
    public SimpleLevelFactory()
    {
    }

    public override ILevel CreateLevel()
    {
        SimpleLevel level = new(32, 32);

        return level; 
    }
}
