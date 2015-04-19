﻿using Ghostbit.Tweaker.Core;
using System;
using System.Collections.Generic;

namespace Ghostbit.Tweaker.UI
{
	public static class HexGridFactory
	{
		public static Dictionary<string, HexGridCell<TCellValue>> MakeRectangleGrid<TCellValue>(uint width, uint height)
			where TCellValue : class
		{
			// array[q][r + q/2]

			var cells = new Dictionary<string, HexGridCell<TCellValue>>();

			// Offsets are to make (0,0) the center of the grid because
			// the grid is generated with (0,0) at the top left.
			int xOffset = (int)width / 2;
			//int yOffset = (int)height;
			//int xOffset = 0;
			int yOffset = 0;

			for (int currentRow = 0; currentRow < height; currentRow++)
			{
				for (int currentColumn = 0; currentColumn < width; ++currentColumn)
				{
					// See http://www.redblobgames.com/grids/hexagons/#map-storage for how this logic
					// was derived.
					// Note: this assume flat top hex tiles

					int q = currentColumn - xOffset;
					int r = currentRow + (currentColumn / 2) - yOffset;
					var cell = new HexGridCell<TCellValue>(new AxialCoord(q, -r), null);
					LogManager.GetCurrentClassLogger().Trace("Next Cell: {0}", cell.AxialCoord);
					cells.Add(cell.AxialCoord.ToString(), cell);
				}
			}

			return cells;
		}

		public static Dictionary<string, HexGridCell<TCellValue>> MakeHexagonGrid<TCellValue>(uint radius)
			where TCellValue : class
		{
			// array[r + N][q + N + min(0, r)]
			throw new NotImplementedException();
		}

		public static Dictionary<string, HexGridCell<TCellValue>> MakeTriangleGrid<TCellValue>(uint sideLength)
			where TCellValue : class
		{
			// Half a Rhombus - if x + y > sideLength then discard cell.
			throw new NotImplementedException();
		}

		public static Dictionary<string, HexGridCell<TCellValue>> MakeRhombusGrid<TCellValue>(uint sideLength)
			where TCellValue : class
		{
			// array[r][q] - Simplest to implement, for(x < size){ for(y < size) { cell = (x,y) } }
			throw new NotImplementedException();
		}
	}
}
