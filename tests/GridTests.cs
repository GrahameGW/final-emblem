using FinalEmblem.Core;
using Godot;
using GdUnit4;
using static GdUnit4.Assertions;
using System;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Tests
{
    [TestSuite]
    public class GridTests
    {
        private Grid GenerateGrid(Vector2I dimensions, Vector3 offset, Vector2 cellSize, bool xy, bool invert)
        {
            var grid = new Grid(dimensions, offset, cellSize, xy, invert);
            for (int x = 0; x < grid.Size.X; x++)
            {
                for (int y = 0; y < grid.Size.Y; y++)
                {
                    grid.CreateTile(new Vector2I(x, y), Terrain.Grass);
                }
            }
            grid.SetAllTileNeighbors();
            return grid;
        }

        [TestCase(10, 10, 0, 0, 0)]
        [TestCase(10, 5, 0, 0, 0)]
        [TestCase(10, 10, 0, 1, 0)]
        [TestCase(10, 10, 1, 0, 0)]
        [TestCase(10, 10, 0, 0, 1)]
        public void GridGenerationDefaultTest(float cellX, float cellY, float x, float y, float z)
        {
            var cellSize = new Vector2(cellX, cellY);
            var grid = GenerateGrid(new Vector2I(10, 10), new Vector3(x, y, z), cellSize, false, false);
            var testTile = grid.Tiles[12];

            AssertObject(testTile.North).IsEqual(grid.Tiles[22]);
            AssertObject(testTile.South).IsEqual(grid.Tiles[2]);
            AssertObject(testTile.East).IsEqual(grid.Tiles[13]);
            AssertObject(testTile.West).IsEqual(grid.Tiles[11]);

            var wp = testTile.WorldPosition.Vector2XY();
            AssertVec2(testTile.North.WorldPosition.Vector2XY()).IsEqual(wp + cellSize * new Vector2(0, 1));
            AssertVec2(testTile.South.WorldPosition.Vector2XY()).IsEqual(wp + cellSize * new Vector2(0, -1));
            AssertVec2(testTile.East.WorldPosition.Vector2XY()).IsEqual(wp + cellSize * new Vector2(1, 0));
            AssertVec2(testTile.West.WorldPosition.Vector2XY()).IsEqual(wp + cellSize * new Vector2(-1, 0));
        }

        [TestCase(10, 10, 0, 0, 0)]
        [TestCase(10, 5, 0, 0, 0)]
        [TestCase(10, 10, 0, 1, 0)]
        [TestCase(10, 10, 1, 0, 0)]
        [TestCase(10, 10, 0, 0, 1)]
        public void GridGenerationXZTest(float cellX, float cellY, float x, float y, float z)
        {
            var cellSize = new Vector2(cellX, cellY);
            var grid = GenerateGrid(new Vector2I(10, 10), new Vector3(x, y, z), cellSize, true, false);
            var testTile = grid.Tiles[12];

            AssertObject(testTile.North).IsEqual(grid.Tiles[22]);
            AssertObject(testTile.South).IsEqual(grid.Tiles[2]);
            AssertObject(testTile.East).IsEqual(grid.Tiles[13]);
            AssertObject(testTile.West).IsEqual(grid.Tiles[11]);

            var wp = testTile.WorldPosition;
            var cellXZ = new Vector3(cellX, 0f, cellY);
            AssertVec3(testTile.North.WorldPosition).IsEqual(wp + cellXZ * new Vector3(0, 0, 1));
            AssertVec3(testTile.South.WorldPosition).IsEqual(wp + cellXZ * new Vector3(0, 0, -1));
            AssertVec3(testTile.East.WorldPosition).IsEqual(wp + cellXZ * new Vector3(1, 0, 0));
            AssertVec3(testTile.West.WorldPosition).IsEqual(wp + cellXZ * new Vector3(-1, 0, 0));
        }

        [TestCase(10, 10, 0, 0, 0)]
        [TestCase(10, 5, 0, 0, 0)]
        [TestCase(10, 10, 0, 1, 0)]
        [TestCase(10, 10, 1, 0, 0)]
        [TestCase(10, 10, 0, 0, 1)]
        public void GridGenerationInvertYTest(float cellX, float cellY, float x, float y, float z)
        {
            var cellSize = new Vector2(cellX, cellY);
            var grid = GenerateGrid(new Vector2I(10, 10), new Vector3(x, y, z), cellSize, false, true);
            var testTile = grid.Tiles[12];

            AssertObject(testTile.South).IsEqual(grid.Tiles[22]);
            AssertObject(testTile.North).IsEqual(grid.Tiles[2]);
            AssertObject(testTile.East).IsEqual(grid.Tiles[13]);
            AssertObject(testTile.West).IsEqual(grid.Tiles[11]);

            var wp = testTile.WorldPosition.Vector2XY();
            AssertVec2(testTile.South.WorldPosition.Vector2XY()).IsEqual(wp + cellSize * new Vector2(0, 1));
            AssertVec2(testTile.North.WorldPosition.Vector2XY()).IsEqual(wp + cellSize * new Vector2(0, -1));
            AssertVec2(testTile.East.WorldPosition.Vector2XY()).IsEqual(wp + cellSize * new Vector2(1, 0));
            AssertVec2(testTile.West.WorldPosition.Vector2XY()).IsEqual(wp + cellSize * new Vector2(-1, 0));
        }

        [TestCase(10, 10, 0, 0, 0)]
        [TestCase(10, 5, 0, 0, 0)]
        [TestCase(10, 10, 0, 1, 0)]
        [TestCase(10, 10, 1, 0, 0)]
        [TestCase(10, 10, 0, 0, 1)]
        public void GridGenerationInvertXZTest(float cellX, float cellY, float x, float y, float z)
        {
            var cellSize = new Vector2(cellX, cellY);
            var grid = GenerateGrid(new Vector2I(10, 10), new Vector3(x, y, z), cellSize, true, true);
            var testTile = grid.Tiles[12];

            AssertObject(testTile.South).IsEqual(grid.Tiles[22]);
            AssertObject(testTile.North).IsEqual(grid.Tiles[2]);
            AssertObject(testTile.East).IsEqual(grid.Tiles[13]);
            AssertObject(testTile.West).IsEqual(grid.Tiles[11]);

            var wp = testTile.WorldPosition;
            var cellXZ = new Vector3(cellX, 0f, cellY);
            AssertVec3(testTile.South.WorldPosition).IsEqual(wp + cellXZ * new Vector3(0, 0, 1));
            AssertVec3(testTile.North.WorldPosition).IsEqual(wp + cellXZ * new Vector3(0, 0, -1));
            AssertVec3(testTile.East.WorldPosition).IsEqual(wp + cellXZ * new Vector3(1, 0, 0));
            AssertVec3(testTile.West.WorldPosition).IsEqual(wp + cellXZ * new Vector3(-1, 0, 0));
        }
    }
}
