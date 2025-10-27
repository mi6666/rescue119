using UnityEngine;

namespace Structure.InGame.Stage
{
    public interface ITipGameObject
    {
        public int InstanceId => InnerObject.InstanceId;
        public InnerObject InnerObject { get; }
    }

    public interface ITipBurnable : ITipGameObject
    {
        public bool IsBurn => InnerBurn.IsBurning;
        public void SetBurn() => InnerBurn.SetBurn();
        public InnerBurn InnerBurn { get; }
    }

    public interface ITipHealth
    {
        public int ObjectHealth => InnerHealth.Health;
        public HealthStateType CurrentState => InnerHealth.State;
        public HealthStateType Damage(int damageAmount) => InnerHealth.Damage(damageAmount);
        public InnerHealth InnerHealth { get; }
    }

    public record InnerObject(int InstanceId);

    public record InnerBurn
    {
        public bool IsBurning => _isBurning;

        public void SetBurn()
        {
            _isBurning = true;
        }

        private bool _isBurning;

        public InnerBurn(bool objectBurn)
        {
            _isBurning = objectBurn;
        }
    }

    public record InnerHealth
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

        private int _health;

        public InnerHealth(int objectHealth)
        {
            _health = objectHealth;
        }
    }

    public enum HealthStateType
    {
        Alive, // 生きている
        Dead, // 死んだ
    }
}