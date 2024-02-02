using System;

public class TileFactoryPresentation : EntityPresentationFactory
{
    public override Type GetEntityType()
    {
        return typeof(Tile);
    }
}
