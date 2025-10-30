using Interface.ModelInterface.InGame;
using Module.Option.Runtime;

namespace Model.InGame.Player
{
    public class PlayerLockModel: IPlayerLockModel
    {
        public OperationHandle GetOperation(string operationMessage)
        {
            return OperationPool.SpawnOperation(operationMessage);
        }

        public bool IsLocked()
        {
            return OperationPool.IsAnyBlocked();
        }

        private OperationPool OperationPool { get; } = new OperationPool();
    }
}