using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameBoard
{
	public Dictionary<HexLocation, GameObject> _board = new Dictionary<HexLocation, GameObject>();

	public GameBoard()
	{
	}

	public GameObject GetTileAt(HexLocation location)
	{
		if (_board.ContainsKey(location)) 
		{
			return _board[location];
		} 
		else 
		{
			return null;
		}
	}

	public GameObject GetAdjacentTile(HexLocation location, BOARD_DIRECTION direction)
	{
		HexLocation adjacentLocation = GetLocationForAdjacentTile(location, direction);

		return GetTileAt (adjacentLocation);
	}

	public void SetTileAt(HexLocation location, GameObject tile)
	{
		TileBehavior tileBehavior = tile.GetComponent<TileBehavior>();
		tileBehavior._TileData.location = location;
		tile.name = string.Format("HexTile ({0},{1})", location.x, location.z);
		_board[location] = tile;
	}

	public HexLocation GetLocationForAdjacentTile(HexLocation location, BOARD_DIRECTION direction) {
		HexLocation adjacentLocation = new HexLocation (location.x, location.z);
		
		switch (direction)
		{
		case BOARD_DIRECTION.NORTH:
			adjacentLocation.z += 1;
			break;
			
		case BOARD_DIRECTION.NORTH_EAST:
			adjacentLocation.x += 1;
			if (adjacentLocation.x % 2 == 0)
			{
				adjacentLocation.z += 1;
			}
			break;
			
		case BOARD_DIRECTION.NORTH_WEST:
			adjacentLocation.x -= 1;
			if (adjacentLocation.x % 2 == 0)
			{
				adjacentLocation.z += 1;
			}
			break;
			
		case BOARD_DIRECTION.SOUTH:
			adjacentLocation.z -= 1;
			break;
			
		case BOARD_DIRECTION.SOUTH_EAST:
			adjacentLocation.x += 1;
			if (adjacentLocation.x % 2 != 0)
			{
				adjacentLocation.z -= 1;
			}
			break;
			
		case BOARD_DIRECTION.SOUTH_WEST:
			adjacentLocation.x -= 1;
			if (adjacentLocation.x % 2 != 0)
			{
				adjacentLocation.z -= 1;
			}
			break;
		}

		return adjacentLocation;
	}


}
