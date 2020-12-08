using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtt.Chess.Core.Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void CreateBoard()
        {
            var b = new Board();
            Assert.AreEqual(64, b.Cells.Count);
            Assert.AreEqual(32, b.Pieces.Count);
        }
    }
}
