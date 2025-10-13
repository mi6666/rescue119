namespace Structure.InGame.Stage
{
    public abstract record TipBase;

    public record NoneTip : TipBase;
    public record FloorTip(InnerBurn InnerBurn, InnerObject InnerObject, InnerHealth InnerHealth) : TipBase, ITipGameObject, ITipBurnable, ITipHealth;
    public record WallTip(InnerBurn InnerBurn, InnerObject InnerObject, InnerHealth InnerHealth) : TipBase, ITipGameObject, ITipBurnable, ITipHealth;
    public record RubbleTip(InnerBurn InnerBurn, InnerObject InnerObject, InnerHealth InnerHealth) : TipBase, ITipGameObject, ITipBurnable, ITipHealth;
    public record HoleTip(InnerObject InnerObject, InnerHealth InnerHealth) : TipBase, ITipGameObject, ITipHealth;
}