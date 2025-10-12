using UnityEngine;

namespace Structure.InGame.Stage
{
    public interface ITipBurnable
    {
        public bool IsBurn => BurnCore.IsBurning;
        public void SetBurn() => BurnCore.SetBurn();
        public BurnCore BurnCore { get; }
    }

    public interface ITipHealth
    {
        public int ObjectHealth => HealthCore.Health;
        public HealthCore.HealthStateType CurrentState => HealthCore.State;
        public HealthCore.HealthStateType Damage(int damageAmount) => HealthCore.Damage(damageAmount);
        public HealthCore HealthCore { get; }
    }

    public record BurnCore
    {
        public bool IsBurning => _isBurning;

        public void SetBurn()
        {
            _isBurning = true;
        }

        private bool _isBurning;
    }

    public record HealthCore
    {
        public int Health => _health;
        public HealthStateType State => _health > 0 ? HealthStateType.Alive : HealthStateType.Dead;

        public HealthStateType Damage(int damageAmount)
        {
            Debug.Assert(damageAmount > 0);
            Debug.Assert(_health > 0);

            _health -= damageAmount;

            return State;
        }

        public enum HealthStateType
        {
            Alive, // 生きている
            Dead, // 死んだ
        }

        private int _health;
    }
}