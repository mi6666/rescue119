namespace Structure.InGame
{
    public enum StageStateType
    {
        EntryPoint,
        Normal,
        FloorTransition,
        Stop,
    }

    public enum StageTileType
    {
        None, // ステージ外
        Floor,
        Wall,
        Rubble, // 瓦礫
        Hole,
    }

    public interface IBurnable
    {
        public void SetBurn();
    }
}