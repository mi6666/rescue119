using System.Collections.Generic;
using Module.Option.Runtime;

namespace Interface.ModelInterface.Global
{
//====================================================================
// Blocking Operation
//====================================================================
    public interface IBlockingOperationModel
    {
        public OperationHandle SpawnOperation(string context);
        public bool IsAnyBlocked();
        public IReadOnlyList<OperationHandle> GetOperationHandles { get; }
    }
}