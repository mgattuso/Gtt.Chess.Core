using Gtt.Chess.Core.Pgn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtt.Chess.Core.Tests
{
    [TestClass]
    public class PgnTurnMoveTests
    {
        [TestMethod]
        public void ExtractPieceFromMovePawnNoNotation()
        {
            var test = "e";
            var result = PieceParser.Parse(test);
            Assert.AreEqual(Piece.Pawn, result);
        }

        [TestMethod]
        public void ExtractPieceFromMovePawnWithPawnNotation()
        {
            var test = "P";
            var result = PieceParser.Parse(test);
            Assert.AreEqual(Piece.Pawn, result);
        }

        [TestMethod]
        public void ExtractPieceFromMoveKnight()
        {
            var test = "N";
            var result = PieceParser.Parse(test);
            Assert.AreEqual(Piece.Knight, result);
        }

        [TestMethod]
        public void ExtractPieceFromMoveRook()
        {
            var test = "R";
            var result = PieceParser.Parse(test);
            Assert.AreEqual(Piece.Rook, result);
        }

        [TestMethod]
        public void ExtractPieceFromMoveBishop()
        {
            var test = "B";
            var result = PieceParser.Parse(test);
            Assert.AreEqual(Piece.Bishop, result);
        }

        [TestMethod]
        public void ExtractPieceFromMoveQueen()
        {
            var test = "Q";
            var result = PieceParser.Parse(test);
            Assert.AreEqual(Piece.Queen, result);
        }

        [TestMethod]
        public void ExtractPieceFromMoveKing()
        {
            var test = "K";
            var result = PieceParser.Parse(test);
            Assert.AreEqual(Piece.King, result);
        }

        [TestMethod]
        public void ExtractDisambiguationFile()
        {
            var test = "Nfd7";
            var result = PgnTurnMove.ExtractDisambiguationFile(test);
            Assert.AreEqual("f", result);
        }

        [TestMethod]
        public void ExtractDisambiguationRank()
        {
            var test = "N8d7";
            var result = PgnTurnMove.ExtractDisambiguationRank(test);
            Assert.AreEqual("8", result);
        }
    }
}