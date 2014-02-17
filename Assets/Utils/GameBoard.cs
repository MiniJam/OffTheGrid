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
		return _board [location];
	}

	public GameObject GetAdjacentTile(HexLocation location, BOARD_DIRECTION direction)
	{
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
			if (adjacentLocation.x % 2 == 1)
			{
				adjacentLocation.z -= 1;
			}
			break;

		case BOARD_DIRECTION.SOUTH_WEST:
			adjacentLocation.x -= 1;
			if (adjacentLocation.x % 2 == 1)
			{
				adjacentLocation.z -= 1;
			}
			break;
		}

		return GetTileAt (adjacentLocation);
	}

	public void SetTileAt(HexLocation location, GameObject tile)
	{
		_board [location] = tile;
	}


}
