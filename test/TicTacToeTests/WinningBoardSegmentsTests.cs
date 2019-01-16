using System;
using System.Collections.Generic;
using Xunit;

namespace TicTacToe.Tests
{
    public class WinningBoardSegmentsTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void CtorInvalidBoardSizeShouldThrowArgumentOutOfRangeException(int boardSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new WinningBoardSegments(boardSize));
        }

        [Theory, MemberData(nameof(GetWinningBoardSegmentLabelsTestData))]
        public void GetWinningBoardSegmentLabelsReturnsWinningBoardSegmentLabels(IEnumerable<string> labels, int col, int row, int boardSize = 3)
        {
            Assert.Equal(labels, new WinningBoardSegments(boardSize).GetWinningBoardSegmentLabels(col, row));
        }

        public static IEnumerable<object[]> GetWinningBoardSegmentLabelsTestData() => new[]
        {
            // column one
            new object[] { new List<string> { "WonColumn0", "WonRow0", "WonDiagonalTop" }, 0, 0 },      // location 0, 0
            new object[] { new List<string> { "WonColumn0", "WonRow1" }, 0, 1 },                        // location 0, 1
            new object[] { new List<string> { "WonColumn0", "WonRow2", "WonDiagonalBottom" }, 0, 2 },   // location 0, 2

            // column two
            new object[] { new List<string> { "WonColumn1", "WonRow0" }, 1, 0 },                                            // location 1, 0
            new object[] { new List<string> { "WonColumn1", "WonRow1", "WonDiagonalTop", "WonDiagonalBottom" }, 1, 1 },     // location 1, 1
            new object[] { new List<string> { "WonColumn1", "WonRow2" }, 1, 2 },                                            // location 1, 2

            // column three
            new object[] { new List<string> { "WonColumn2", "WonRow0", "WonDiagonalBottom" }, 2, 0 },   // location 2, 0
            new object[] { new List<string> { "WonColumn2", "WonRow1" }, 2, 1 },                        // location 2, 1
            new object[] { new List<string> { "WonColumn2", "WonRow2", "WonDiagonalTop" }, 2, 2 },      // location 2, 2

            // column four
            new object[] { new List<string> { "WonColumn3", "WonRow0", "WonDiagonalBottom" }, 3, 0, 4 },   // location 3, 0
            new object[] { new List<string> { "WonColumn3", "WonRow1" }, 3, 1, 4 },                        // location 3, 1
            new object[] { new List<string> { "WonColumn3", "WonRow2" }, 3, 2, 4 },                        // location 3, 2
            new object[] { new List<string> { "WonColumn3", "WonRow3", "WonDiagonalTop" }, 3, 3, 4 },      // location 3, 3

            // row four
            new object[] { new List<string> { "WonColumn0", "WonRow3", "WonDiagonalBottom" }, 0, 3, 4 },    // location 0, 3
            new object[] { new List<string> { "WonColumn1", "WonRow3" }, 1, 3, 4 },                         // location 1, 3
            new object[] { new List<string> { "WonColumn2", "WonRow3" }, 2, 3, 4 },                         // location 2, 3
            new object[] { new List<string> { "WonColumn3", "WonRow3", "WonDiagonalTop" }, 3, 3, 4 }        // location 3, 3
        };

        [Theory, MemberData(nameof(GetWinningBoardSegmentLabelsWithDiamondsTestData))]
        public void GetWinningBoardSegmentLabelsWithDiamondsReturnsWinningBoardSegmentLabels(IEnumerable<string> labels, int col, int row)
        {
            Assert.Equal(labels, new WinningBoardSegments(boardSize: 4, includeDiamonds: true).GetWinningBoardSegmentLabels(col, row));
        }

        public static IEnumerable<object[]> GetWinningBoardSegmentLabelsWithDiamondsTestData() => new[]
        {
            new object[] { new List<string> { "WonColumn1", "WonRow0", "WonDiamond10" }, 1, 0 },                                       // location 1, 0
            new object[] { new List<string> { "WonColumn2", "WonRow0", "WonDiamond20" }, 2, 0 },                                       // location 2, 0
            new object[] { new List<string> { "WonColumn0", "WonRow1", "WonDiamond10" }, 0, 1 },                                       // location 0, 1
            new object[] { new List<string> { "WonColumn1", "WonRow1", "WonDiagonalTop", "WonDiamond11", "WonDiamond20" }, 1, 1 },     // location 1, 1
            new object[] { new List<string> { "WonColumn2", "WonRow1", "WonDiagonalBottom", "WonDiamond10", "WonDiamond21" }, 2, 1 },  // location 2, 1
            new object[] { new List<string> { "WonColumn3", "WonRow1", "WonDiamond20" }, 3, 1 },                                       // location 3, 1
            new object[] { new List<string> { "WonColumn0", "WonRow2", "WonDiamond11" }, 0, 2 },                                       // location 0, 2
            new object[] { new List<string> { "WonColumn1", "WonRow2", "WonDiagonalBottom", "WonDiamond10", "WonDiamond21" }, 1, 2 },  // location 1, 2
            new object[] { new List<string> { "WonColumn2", "WonRow2", "WonDiagonalTop", "WonDiamond11", "WonDiamond20" }, 2, 2 },     // location 2, 2
            new object[] { new List<string> { "WonColumn3", "WonRow2", "WonDiamond21" }, 3, 2 },                                       // location 3, 2
            new object[] { new List<string> { "WonColumn1", "WonRow3", "WonDiamond11" }, 1, 3 },                                       // location 1, 3
            new object[] { new List<string> { "WonColumn2", "WonRow3", "WonDiamond21" }, 2, 3 },                                       // location 2, 3
        };

        [Theory, MemberData(nameof(GetWinningBoardSegmentLabelsWithSquaresTestData))]
        public void GetWinningBoardSegmentLabelsWithSquaresReturnsWinningBoardSegmentLabels(IEnumerable<string> labels, int col, int row)
        {
            Assert.Equal(labels, new WinningBoardSegments(boardSize: 4, includeSquares: true).GetWinningBoardSegmentLabels(col, row));
        }

        public static IEnumerable<object[]> GetWinningBoardSegmentLabelsWithSquaresTestData() => new[]
        {
            new object[] { new List<string> { "WonColumn0", "WonRow0", "WonDiagonalTop", "WonSquare00" }, 0, 0,  },                                                   // location 0, 0
            new object[] { new List<string> { "WonColumn1", "WonRow0", "WonSquare00", "WonSquare10" }, 1, 0,  },                                                      // location 1, 0
            new object[] { new List<string> { "WonColumn2", "WonRow0", "WonSquare10", "WonSquare20" }, 2, 0,  },                                                      // location 2, 0
            new object[] { new List<string> { "WonColumn3", "WonRow0", "WonDiagonalBottom", "WonSquare20" }, 3, 0,  },                                                // location 3, 0
            new object[] { new List<string> { "WonColumn0", "WonRow1", "WonSquare00", "WonSquare01" }, 0, 1,  },                                                      // location 0, 1
            new object[] { new List<string> { "WonColumn1", "WonRow1", "WonDiagonalTop", "WonSquare00", "WonSquare01", "WonSquare10", "WonSquare11" }, 1, 1,  },      // location 1, 1
            new object[] { new List<string> { "WonColumn2", "WonRow1", "WonDiagonalBottom", "WonSquare10", "WonSquare11", "WonSquare20", "WonSquare21" }, 2, 1,  },   // location 2, 1
            new object[] { new List<string> { "WonColumn3", "WonRow1", "WonSquare20", "WonSquare21" }, 3, 1,  },                                                      // location 3, 1
            new object[] { new List<string> { "WonColumn0", "WonRow2", "WonSquare01", "WonSquare02" }, 0, 2,  },                                                      // location 0, 2
            new object[] { new List<string> { "WonColumn1", "WonRow2", "WonDiagonalBottom", "WonSquare01", "WonSquare02", "WonSquare11", "WonSquare12" }, 1, 2,  },   // location 1, 2
            new object[] { new List<string> { "WonColumn2", "WonRow2", "WonDiagonalTop", "WonSquare11", "WonSquare12", "WonSquare21", "WonSquare22" }, 2, 2,  },      // location 2, 2
            new object[] { new List<string> { "WonColumn3", "WonRow2", "WonSquare21", "WonSquare22" }, 3, 2,  },                                                      // location 3, 2
            new object[] { new List<string> { "WonColumn0", "WonRow3", "WonDiagonalBottom", "WonSquare02" }, 0, 3,  },                                                // location 0, 3
            new object[] { new List<string> { "WonColumn1", "WonRow3", "WonSquare02", "WonSquare12" }, 1, 3,  },                                                      // location 1, 3
            new object[] { new List<string> { "WonColumn2", "WonRow3", "WonSquare12", "WonSquare22" }, 2, 3,  },                                                      // location 2, 3
            new object[] { new List<string> { "WonColumn3", "WonRow3", "WonDiagonalTop", "WonSquare22" }, 3, 3,  },                                                   // location 3, 3
        };
    }
}
