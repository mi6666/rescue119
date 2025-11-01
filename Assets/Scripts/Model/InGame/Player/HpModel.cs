using Interface.ModelInterface.InGame;
using R3;

namespace Model.InGame.Player
{
    public class HpModel: IHpModel
    {
        public HpModel(IHpSetting hpSetting)
        {
            HpSetting = hpSetting;
            Hp.Value = hpSetting.MaxHp;
        }

        public int CurrentHp => Hp.CurrentValue;

        private ReactiveProperty<int> Hp { get; } = new ();
        private IHpSetting HpSetting { get; }
    }
}