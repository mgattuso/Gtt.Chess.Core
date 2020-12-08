using Gtt.Chess.Core.Pgn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtt.Chess.Core.Tests
{
    [TestClass]
    public class PgnTurnMovePromotionTests
    {
        [TestMethod]
        public void PromotionHappyPath()
        {
            var text = "e4=Q";
            var result = new PgnTurnMovePromotion(text);
            Assert.AreEqual(result.PromotedTo, Piece.Queen);
        }
    }
}