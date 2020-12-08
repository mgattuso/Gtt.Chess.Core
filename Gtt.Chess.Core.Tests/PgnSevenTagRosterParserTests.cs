using System;
using System.Collections.Generic;
using System.Text;
using Gtt.Chess.Core.Pgn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtt.Chess.Core.Tests
{
    [TestClass]
    public class PgnSevenTagRosterParserTests
    {

        [TestMethod]
        public void HappyPath()
        {
            var test = @"
[Event ""Lloyds Bank op""]
[Site ""London""]
[Date ""1984.??.??""]
[Round ""1""]
[White ""Adams, Michael""]
[Black ""Sedgwick, David""]
[Result ""1-0""]
[WhiteElo """"]
[BlackElo """"]
[ECO ""C05""]

";
            var result = PgnSevenTagRosterParser.Parse(test);
            Assert.AreEqual("Lloyds Bank op", result.Event);
            Assert.AreEqual("London", result.Site);
            Assert.AreEqual("1984.??.??", result.Date);
            Assert.AreEqual("1", result.Round);
            Assert.AreEqual("Adams, Michael", result.White);
            Assert.AreEqual("Sedgwick, David", result.Black);
            Assert.AreEqual("1-0", result.Result);

        }
    }
}
