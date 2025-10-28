using NUnit.Framework;
using Model.InGame.Stage;

namespace Test.EditModeTest.Model.InGame.Stage
{
    public class StagePawnModelTest
    {
        private StagePawnModel _stagePawnModel;
        
        [SetUp]
        public void SetUp()
        {
            _stagePawnModel = new StagePawnModel();
        }
        //
        // [Test]
        // public void StorePawn_ShouldAddPawn()
        // {
        //     var pawn = new GridCollider(1, 0, new Vector2Int(0, 0), new Vector2Int(1, 1));
        //     _stagePawnModel.StorePawn(pawn);
        //
        //     var result = _stagePawnModel.CastPosition(new Vector2Int(0, 0));
        //
        //     Assert.IsTrue(result.IsSome);
        //     Assert.AreEqual(1, result.Unwrap());
        // }
        //
        // [Test]
        // public void RemovePawn_ShouldRemovePawn()
        // {
        //     var pawn = new GridCollider(1, 0, new Vector2Int(0, 0), new Vector2Int(1, 1));
        //     _stagePawnModel.StorePawn(pawn);
        //     _stagePawnModel.RemovePawn(1);
        //
        //     var result = _stagePawnModel.CastPosition(new Vector2Int(0, 0));
        //
        //     Assert.IsFalse(result.IsSome);
        // }
        //
        // [Test]
        // public void RemovePawn_NonExistent_ShouldNotThrowError()
        // {
        //     Assert.DoesNotThrow(() => _stagePawnModel.RemovePawn(999));
        // }
        //
        // [Test]
        // public void CastPosition_OnEmptyModel_ShouldReturnNone()
        // {
        //     var result = _stagePawnModel.CastPosition(new Vector2Int(0, 0));
        //     Assert.IsFalse(result.IsSome);
        // }
        //
        // [Test]
        // public void CastPosition_OnOccupiedCell_ShouldReturnPawnId()
        // {
        //     var pawn = new GridCollider(1, 0, new Vector2Int(5, 5), new Vector2Int(3, 3));
        //     _stagePawnModel.StorePawn(pawn);
        //
        //     var result = _stagePawnModel.CastPosition(new Vector2Int(6, 6));
        //
        //     Assert.IsTrue(result.IsSome);
        //     Assert.AreEqual(1, result.Unwrap());
        // }
        //
        // [Test]
        // public void CastPosition_OnCellWithMultipleOverlappingPawns_ShouldReturnOneId()
        // {
        //     var pawn1 = new GridCollider(1, 0, new Vector2Int(2, 2), new Vector2Int(2, 2));
        //     var pawn2 = new GridCollider(2, 0, new Vector2Int(3, 3), new Vector2Int(2, 2));
        //     _stagePawnModel.StorePawn(pawn1);
        //     _stagePawnModel.StorePawn(pawn2);
        //
        //     var result = _stagePawnModel.CastPosition(new Vector2Int(3, 3));
        //
        //     Assert.IsTrue(result.IsSome);
        //     Assert.IsTrue(result.Unwrap() == 1 || result.Unwrap() == 2);
        // }
        //
        // [Test]
        // [TestCase(0, 0)]
        // [TestCase(1, 0)]
        // [TestCase(0, 1)]
        // [TestCase(1, 1)]
        // public void CastPosition_OnEdge_ShouldReturnPawnId(int x, int y)
        // {
        //     var pawn = new GridCollider(1, 0, new Vector2Int(0, 0), new Vector2Int(2, 2));
        //     _stagePawnModel.StorePawn(pawn);
        //
        //     var result = _stagePawnModel.CastPosition(new Vector2Int(x, y));
        //
        //     Assert.IsTrue(result.IsSome);
        //     Assert.AreEqual(1, result.Unwrap());
        // }
        //
        // [Test]
        // [TestCase(2, 0)]
        // [TestCase(0, 2)]
        // [TestCase(2, 2)]
        // [TestCase(-1, 0)]
        // [TestCase(0, -1)]
        // public void CastPosition_OutsideOfPawn_ShouldReturnNone(int x, int y)
        // {
        //     var pawn = new GridCollider(1, 0, new Vector2Int(0, 0), new Vector2Int(2, 2));
        //     _stagePawnModel.StorePawn(pawn);
        //
        //     var result = _stagePawnModel.CastPosition(new Vector2Int(x, y));
        //
        //     Assert.IsFalse(result.IsSome);
        // }
    }
}