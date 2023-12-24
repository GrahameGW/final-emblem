using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using GdUnit4;
using static GdUnit4.Assertions;

namespace FinalEmblem.Tests
{
	[TestSuite]
	public class TileTests
	{
		private List<Tile> tiles = new();

		[Before]
		public void Setup()
		{
			tiles = new()
			{
				new(new Vector2I(2, 3), Terrain.Grass),
				new(new Vector2I(3, 4), Terrain.Grass),
				new(new Vector2I(18, 4), Terrain.Grass)
			};
		}

		[TestCase]
		public void SetTileWorldPosDefault()
		{
			var offset = new Vector3(1.5f, 3f, 5f);
			var cellSize = new Vector2(1f, 1f);

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize);
				Vector3 expected = new(tile.Coordinates.X, tile.Coordinates.Y, 0f);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize);
				Vector3 expected = new(tile.Coordinates.X, tile.Coordinates.Y, 0f);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(offset, cellSize);
				Vector3 expected = new Vector3(tile.Coordinates.X, tile.Coordinates.Y, 0f) + offset;
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize * 2f);
				Vector3 expected = new(tile.Coordinates.X * 2f, tile.Coordinates.Y * 2f, 0f);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}
		}

		[TestCase]
		public void SetTileWorldPosInvertY()
		{
			var offset = new Vector3(1.5f, 3f, 5f);
			var cellSize = new Vector2(1f, 1f);

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize, invertY: true);
				Vector3 expected = new(tile.Coordinates.X, -tile.Coordinates.Y, 0f);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize, invertY: true);
				Vector3 expected = new(tile.Coordinates.X, -tile.Coordinates.Y, 0f);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(offset, cellSize, invertY: true);
				Vector3 expected = new Vector3(tile.Coordinates.X, -tile.Coordinates.Y, 0f) + offset;
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize * 2f, invertY: true);
				Vector3 expected = new(tile.Coordinates.X * 2f, -tile.Coordinates.Y * 2f, 0f);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}
		}

		[TestCase]
		public void SetTileWorldPosXZOrientation()
		{
			var offset = new Vector3(1.5f, 3f, 5f);
			var cellSize = new Vector2(1f, 1f);

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize, isXZ: true);
				Vector3 expected = new(tile.Coordinates.X, 0f, tile.Coordinates.Y);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize, isXZ: true);
				Vector3 expected = new(tile.Coordinates.X, 0f, tile.Coordinates.Y);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(offset, cellSize, isXZ: true);
				Vector3 expected = new Vector3(tile.Coordinates.X, 0f, tile.Coordinates.Y) + offset;
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize * 2f, isXZ: true);
				Vector3 expected = new(tile.Coordinates.X * 2f, 0f, tile.Coordinates.Y * 2f);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}
		}

		[TestCase]
		public void SetTileWorldPosXZOrientationInvert()
		{
			var offset = new Vector3(1.5f, 3f, 5f);
			var cellSize = new Vector2(1f, 1f);

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize, isXZ: true, invertY: true);
				Vector3 expected = new(tile.Coordinates.X, 0f, -tile.Coordinates.Y);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize, isXZ: true, invertY: true);
				Vector3 expected = new(tile.Coordinates.X, 0f, -tile.Coordinates.Y);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(offset, cellSize, isXZ: true, invertY: true);
				Vector3 expected = new Vector3(tile.Coordinates.X, 0f, -tile.Coordinates.Y) + offset;
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}

			foreach (var tile in tiles)
			{
				tile.SetWorldPosition(Vector3.Zero, cellSize * 2f, isXZ: true, invertY: true);
				Vector3 expected = new(tile.Coordinates.X * 2f, 0f, -tile.Coordinates.Y * 2f);
				AssertThat(expected).IsEqual(tile.WorldPosition);
			}
		}
	}
}
