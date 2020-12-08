using System.Linq;
using Gtt.Chess.Core.Pgn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtt.Chess.Core.Tests
{
    [TestClass]
    public class PgnParserTests
    {
        [TestMethod]
        public void ExtractGame()
        {
            var completeGame = @"
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

1.e4 e6 2.d4 d5 3.Nd2 Nf6 4.e5 Nfd7 5.f4 c5 6.c3 Nc6 7.Ndf3 cxd4 8.cxd4 f6
9.Bd3 Bb4+ 10.Bd2 Qb6 11.Ne2 fxe5 12.fxe5 O-O 13.a3 Be7 14.Qc2 Rxf3 15.gxf3 Nxd4
16.Nxd4 Qxd4 17.O-O-O Nxe5 18.Bxh7+ Kh8 19.Kb1 Qh4 20.Bc3 Bf6 21.f4 Nc4 22.Bxf6 Qxf6
23.Bd3 b5 24.Qe2 Bd7 25.Rhg1 Be8 26.Rde1 Bf7 27.Rg3 Rc8 28.Reg1 Nd6 29.Rxg7 Nf5
30.R7g5 Rc7 31.Bxf5 exf5 32.Rh5+  1-0
";

            var game = PgnParser.ParseGame(completeGame);
            Assert.AreEqual(32, game.Turns.Count());
        }

        [TestMethod]
        public void GameImport2()
        {
            var completeGame = @"
[Event ""World Chess Woman Summit""]
[Site ""chess.com INT""]
[Date ""2020.12.04""]
[Round ""1""]
[White ""Lei Tingjie""]
[Black ""Abdumalik,Z""]
[Result ""1-0""]
[WhiteElo ""2505""]
[BlackElo ""2478""]
[EventDate ""2020.12.04""]
[ECO ""E71""]

1. d4 Nf6 2. c4 g6 3. Nc3 d6 4. e4 Bg7 5. h3 c6 6. Be3 O-O 7. Nf3 e5 8. d5
Nh5 9. Nd2 Nd7 10. g3 a5 11. Be2 Nhf6 12. g4 Ne8 13. h4 Nc5 14. g5 cxd5 15.
exd5 f5 16. Qc2 Na6 17. f3 f4 18. Bf2 Bf5 19. Nde4 Rc8 20. O-O-O Rf7 21.
Kb1 b6 22. Ka1 Nc5 23. Bxc5 bxc5 24. Nb5 Bf8 25. Bd3 Be7 26. h5 Ng7 27.
Rdg1 Qd7 28. hxg6 hxg6 29. Rh6 Rff8 30. Nbxd6 Bxd6 31. Nf6+ Rxf6 32. gxf6
Bxd3 33. Qxd3 Qf5 34. Rgxg6 1-0
";
        }
    }
}
