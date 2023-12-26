using GdUnit4;
using static GdUnit4.Assertions;
using FinalEmblem.Core;
using Godot;
using System.Collections.Generic;
using static Godot.HttpRequest;

namespace FinalEmblem.Tests
{
	[TestSuite]
	public class TileExtensionTests
	{
		private Tile originTile;
		private Tile randomTile;
		private List<(Tile, Compass)> testCases;

		[Before] 
		public void Before()
		{
            originTile = new Tile(new Vector2I(0, 0), Terrain.Grass);
			var rand = new Vector2I(GD.RandRange(-100, 100), GD.RandRange(-100, 100));
			randomTile = new Tile(rand, Terrain.Grass);
        }

        [TestCase(2, 0, Compass.E)]
        [TestCase(2, 2, Compass.NE)]
        [TestCase(2, -2, Compass.SE)]
        [TestCase(2, 1, Compass.E)]
        [TestCase(2, -1, Compass.E)]
        [TestCase(2, 3, Compass.N)]
        [TestCase(2, -3, Compass.S)]
        [TestCase(-2, 0, Compass.W)]
        [TestCase(-2, 2, Compass.NW)]
        [TestCase(-2, -2, Compass.SW)]
        [TestCase(-2, 1, Compass.W)]
        [TestCase(-2, -1, Compass.W)]
        [TestCase(-2, 3, Compass.N)]
        [TestCase(-2, -3, Compass.S)]
        public void DirectionToApproxDiagsDefault(int x, int y, Compass result) 
		{
            var dir = new Vector2I(x, y);
            var orignOther = new Tile(originTile.Coordinates + dir, Terrain.Grass);
            var randOther = new Tile(randomTile.Coordinates + dir, Terrain.Grass);

            AssertInt((int)originTile.DirectionToApproxDiagonals(orignOther)).IsEqual((int)result);
            AssertInt((int)randomTile.DirectionToApproxDiagonals(randOther)).IsEqual((int)result);
		}

        [TestCase(2, 0, Compass.E)]
        [TestCase(2, -2, Compass.NE)]
        [TestCase(2, 2, Compass.SE)]
        [TestCase(2, -1, Compass.E)]
        [TestCase(2, 1, Compass.E)]
        [TestCase(2, -3, Compass.N)]
        [TestCase(2, 3, Compass.S)]
        [TestCase(-2, 0, Compass.W)]
        [TestCase(-2, -2, Compass.NW)]
        [TestCase(-2, 2, Compass.SW)]
        [TestCase(-2, -1, Compass.W)]
        [TestCase(-2, 1, Compass.W)]
        [TestCase(-2, -3, Compass.N)]
        [TestCase(-2, 3, Compass.S)]
        public void DirectionToApproxDiagsInvertY(int x, int y, Compass result)
		{
            var dir = new Vector2I(x, y);
            var orignOther = new Tile(originTile.Coordinates + dir, Terrain.Grass);
            var randOther = new Tile(randomTile.Coordinates + dir, Terrain.Grass);

            AssertInt((int)originTile.DirectionToApproxDiagonals(orignOther, true)).IsEqual((int)result);
            AssertInt((int)randomTile.DirectionToApproxDiagonals(randOther, true)).IsEqual((int)result);
        }

        [TestCase]
        public void DirectionToApproxDiagsThrowsOnSameTile()
		{
            AssertThrown(() => originTile.DirectionToApproxDiagonals(originTile));
            AssertThrown(() => originTile.DirectionToApproxDiagonals(originTile, true));
            AssertThrown(() => randomTile.DirectionToApproxDiagonals(randomTile));
            AssertThrown(() => randomTile.DirectionToApproxDiagonals(randomTile, true));
        }
	}
}
