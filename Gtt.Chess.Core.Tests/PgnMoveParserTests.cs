using System;
using System.Collections.Generic;
using System.Text;
using Gtt.Chess.Core.Pgn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtt.Chess.Core.Tests
{
    [TestClass]
    public class PgnMoveParserTests
    {

        [TestMethod]
        public void IsMove_Valid_Move()
        {
            // arrange
            var test = "a1";
            // act
            var result = PgnMoveTextParser.IsMove(test);
            // assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void IsMove_Invalid()
        {
            // arrange
            var test = "1.";
            // act
            var result = PgnMoveTextParser.IsMove(test);
            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsMove_Valid_With_Move_Number()
        {
            // arrange
            var test = "1.a1";
            // act
            var result = PgnMoveTextParser.IsMove(test);
            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ConvertToTokens_Null_Returns_EmptyArray()
        {
            string test = null;
            string[] results = PgnMoveTextParser.ConvertToTokens(test);
            Assert.AreEqual(0, results.Length);
        }

        [TestMethod]
        public void ConvertToTokens_Empty_Returns_EmptyArray()
        {
            string test = "";
            string[] results = PgnMoveTextParser.ConvertToTokens(test);
            Assert.AreEqual(0, results.Length);
        }

        [TestMethod]
        public void ConvertToTokens_Whitespace_Returns_EmptyArray()
        {
            string test = " ";
            string[] results = PgnMoveTextParser.ConvertToTokens(test);
            Assert.AreEqual(0, results.Length);
        }

        [TestMethod]
        public void ConvertToTokens_Space_Delimited_Return_Array()
        {
            string test = "e4 a6";
            string[] results = PgnMoveTextParser.ConvertToTokens(test);
            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("e4", results[0]);
            Assert.AreEqual("a6", results[1]);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void NotationStyle_Null_Array()
        {
            string[] test = null;
            var result = PgnMoveTextParser.NotationStyle(test);
            Assert.AreEqual("3", result);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void NotationStyle_Less_Than_Minimum_Parts_Array()
        {
            string[] test = new string[0];
            var result = PgnMoveTextParser.NotationStyle(test);
            Assert.AreEqual("3", result);
        }

        [TestMethod]
        public void NotationStyle_Three_Notation()
        {
            string[] test = { "1.", "a4", "e3" };
            var result = PgnMoveTextParser.NotationStyle(test);
            Assert.AreEqual("3", result);
        }

        [TestMethod]
        public void NotationStyle_Three_Join_With_Number_Notation()
        {
            string[] test = { "1.a4", "e3" };
            var result = PgnMoveTextParser.NotationStyle(test);
            Assert.AreEqual("2", result);
        }

        [TestMethod]
        public void NotationStyle_Two_Notation()
        {
            string[] test = { "a4", "e3" };
            var result = PgnMoveTextParser.NotationStyle(test);
            Assert.AreEqual("2", result);
        }

        [TestMethod]
        public void GetMoves_Two_Format()
        {
            string[] test = { "a4", "e3" };
            var result = PgnMoveTextParser.GetMoveGroups(test);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(2, result[0].Length);
            Assert.AreEqual("a4", result[0][0]);
            Assert.AreEqual("e3", result[0][1]);
        }

        [TestMethod]
        public void GetMoves_Three_Format()
        {
            string[] test = { "1.", "a4", "e3" };
            var result = PgnMoveTextParser.GetMoveGroups(test);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(3, result[0].Length);
            Assert.AreEqual("1.", result[0][0]);
            Assert.AreEqual("a4", result[0][1]);
            Assert.AreEqual("e3", result[0][2]);
        }

        [TestMethod]
        public void GetMoveNumber_NoMoveNumber()
        {
            string[] test = { "a3", "e6" };
            int? number = PgnMoveTextParser.GetMoveNumber(test);
            Assert.IsNull(number);
        }

        [TestMethod]
        public void GetMoveNumber_MoveNumberOnly_With_Dot()
        {
            string[] test = { "1.", "a3", "e6" };
            int? number = PgnMoveTextParser.GetMoveNumber(test);
            Assert.AreEqual(1, number);
        }

        [TestMethod]
        public void GetMoveNumber_MoveNumberOnly_Without_Dot()
        {
            string[] test = { "999", "a3", "e6" };
            int? number = PgnMoveTextParser.GetMoveNumber(test);
            Assert.AreEqual(999, number);
        }

        [TestMethod]
        public void GetMoveNumber_MoveNumberWithMove_With_Dot()
        {
            string[] test = { "1.a3", "e6" };
            int? number = PgnMoveTextParser.GetMoveNumber(test);
            Assert.AreEqual(1, number);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetMoveNumber_ExceptionPath()
        {
            string[] test = { "1.a3", "e6", "r4", "bv" };
            int? number = PgnMoveTextParser.GetMoveNumber(test);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetMoveNumber_ExceptionPath2()
        {
            string[] test = { "1.a3" };
            int? number = PgnMoveTextParser.GetMoveNumber(test);
        }

        [TestMethod]
        public void ExtractMove_WithNoNumberingNotation()
        {
            string test = "a3";
            string result = PgnMoveTextParser.ExtractMove(test);
            Assert.AreEqual("a3", result);
        }

        [TestMethod]
        public void ExtractMove_WithNumberingNotation()
        {
            string test = "a3";
            string result = PgnMoveTextParser.ExtractMove(test);
            Assert.AreEqual("a3", result);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ExtractMove_WithNumberingNotation_Null_Exception()
        {
            string test = "";
            string result = PgnMoveTextParser.ExtractMove(test);
        }

        [TestMethod]
        public void NormalizeMoveGroup_Two_Elements()
        {
            string[] test = { "a4", "e4" };
            var result = PgnMoveTextParser.NormalizeTurn(test, 0);
            Assert.AreEqual(1, result.turnNumber);
            Assert.AreEqual("a4", result.white);
            Assert.AreEqual("e4", result.black);
        }

        [TestMethod]
        public void NormalizeMoveGroup_Two_Elements_With_MoveNumber()
        {
            string[] test = { "1.a4", "e4" };
            var result = PgnMoveTextParser.NormalizeTurn(test, 1);
            Assert.AreEqual(1, result.turnNumber);
            Assert.AreEqual("a4", result.white);
            Assert.AreEqual("e4", result.black);
        }

        [TestMethod]
        public void NormalizeMoveGroup_Three_Elements_With_Period()
        {
            string[] test = { "1.", "a4", "e4" };
            var result = PgnMoveTextParser.NormalizeTurn(test, 99);
            Assert.AreEqual(1, result.turnNumber);
            Assert.AreEqual("a4", result.white);
            Assert.AreEqual("e4", result.black);
        }

        [TestMethod]
        public void NormalizeMoveGroup_Three_Elements_Without_Period()
        {
            string[] test = { "1", "a4", "e4" };
            var result = PgnMoveTextParser.NormalizeTurn(test, 99);
            Assert.AreEqual(1, result.turnNumber);
            Assert.AreEqual("a4", result.white);
            Assert.AreEqual("e4", result.black);
        }

        [TestMethod]
        public void NormalizeMoveGroup_Take_Turn_Number_FromIteration()
        {
            string[] test = { "a4", "e4" };
            var result = PgnMoveTextParser.NormalizeTurn(test, 98);
            Assert.AreEqual(99, result.turnNumber); // move index is zero based so add one for the expected move number
            Assert.AreEqual("a4", result.white);
            Assert.AreEqual("e4", result.black);
        }

        [TestMethod]
        public void ExtractSquareFromMove_Happy()
        {
            string[] tests =
                {
                    "a4", "a4", "w",
                    "Nbxd6", "d6", "w",
                    "Qxd3", "d3", "w",
                    "Bxd6","d6", "w",
                    "O-O","g1", "w",
                    "O-O-O","c1", "w",
                    "O-O","g8", "b",
                    "O-O-O","c8", "b",
                    "Nf3","f3", "w",
                    "Pa4","a4", "w",
                    "Nf6+","f6", "w",
                    "1-0","", "w"
                };

            for (int i = 0; i < tests.Length; i += 3)
            {
                var start = i % 3 == 0;
                if (start)
                {

                    var test = tests[i];
                    var expected = tests[i + 1];
                    var color = tests[i + 2] == "w" ? Color.White : Color.Black;
                    var result = PgnMoveTextParser.ExtractSquareFromMove(test, color);
                    Assert.AreEqual(expected, result, $"{test} should return {expected}. Got {result}");
                }
            }
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void ExtractSquareFromMove_Exception()
        {
            var test = "i1";
            var result = PgnMoveTextParser.ExtractSquareFromMove(test, Color.White);
        }

        [TestMethod]
        public void Parse_Happy()
        {
            var moveText = @"
1.e4 e6 2.d4 d5 3.Nd2 Nf6 4.e5 Nfd7 5.f4 c5 6.c3 Nc6 7.Ndf3 cxd4 8.cxd4 f6
9.Bd3 Bb4+ 10.Bd2 Qb6 11.Ne2 fxe5 12.fxe5 O-O 13.a3 Be7 14.Qc2 Rxf3 15.gxf3 Nxd4
16.Nxd4 Qxd4 17.O-O-O Nxe5 18.Bxh7+ Kh8 19.Kb1 Qh4 20.Bc3 Bf6 21.f4 Nc4 22.Bxf6 Qxf6
23.Bd3 b5 24.Qe2 Bd7 25.Rhg1 Be8 26.Rde1 Bf7 27.Rg3 Rc8 28.Reg1 Nd6 29.Rxg7 Nf5
30.R7g5 Rc7 31.Bxf5 exf5 32.Rh5+  1-0
";
            IEnumerable<PgnTurn> result = PgnMoveTextParser.Parse(moveText);
            var a = 1;
        }
    }
}
