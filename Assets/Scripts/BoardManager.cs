using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour 
{
	private const float X_OFFSET = 0.864f;  // 0.866f
	private const float Z_OFFSET = 0.499f; // 0.5f

	public GameObject _emptyGridPrefab;
	public GameObject _cityGridPrefab;
	public GameObject _cityGridVisualization;
	public GameBoard _gameBoard;
	public GameObject _currentHex;
	public GestureDetector _gestureDetector;

	bool initialGridGenerated;

	void Awake()
	{
		_gameBoard = new GameBoard();
		_gestureDetector = new GestureDetector();
	}

	// Use this for initialization
	void Start ()
	{
		// generate empty tiles 5 deep around the starting location
		GameObject initialTile = Instantiate (_cityGridPrefab) as GameObject;
		_gameBoard.SetTileAt(new HexLocation(0,0), initialTile);
		initialTile.transform.parent = this.transform;
		SetCurrentHex (initialTile);
	}
	
	// Update is called once per frame
	void Update ()
	{
		_gestureDetector.Update();
		if (_gestureDetector.IsClick())
		{
			Ray touchTestRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(touchTestRay, out hitInfo))
			{
				GameObject touchedObject = hitInfo.collider.gameObject;
				TileBehavior tile = touchedObject.GetComponent<TileBehavior>();
				if (null != tile) 
				{
					SetCurrentHex(touchedObject);
				}
			}
		}
	}


	
	void GenerateBoardAroundTile(HexLocation location) {
		List<HexLocation> newLocations = new List<HexLocation>();

		for (BOARD_DIRECTION direction = BOARD_DIRECTION.NORTH;
		     direction != BOARD_DIRECTION.MAX_DIRECTIONS;
		     ++direction)
		{
			HexLocation newLocation = _gameBoard.GetLocationForAdjacentTile(location, direction);
			newLocations.Add(newLocation);

			GameObject adjTile = _gameBoard.GetAdjacentTile(location, direction);
			if (adjTile != null) 
			{
				var adjTileBehavior = adjTile.GetComponent<TileBehavior>();
				if (adjTileBehavior.revealed == false) {
					GameObject newVis = Instantiate(_cityGridVisualization) as GameObject;
					adjTileBehavior.RevealTile(newVis);
				}
				continue;
			}

			GameObject newTile = Instantiate(_cityGridPrefab) as GameObject;
			newTile.transform.parent = this.transform;
			newTile.transform.position = PositionForHexLocation(newLocation);
			_gameBoard.SetTileAt(newLocation, newTile);
			TileBehavior newTileBehavior = newTile.GetComponent<TileBehavior>();
			if (newTileBehavior != null) {
				newTileBehavior._TileData.location = newLocation;
			}
		}

		foreach (HexLocation loc in newLocations) {
			for (BOARD_DIRECTION direction = BOARD_DIRECTION.NORTH;
			     direction != BOARD_DIRECTION.MAX_DIRECTIONS;
			     ++direction)
			{
				GameObject adjTile = _gameBoard.GetAdjacentTile(loc, direction);
				if (adjTile != null) 
				{
					continue;
				}

				HexLocation newLocation = _gameBoard.GetLocationForAdjacentTile(loc, direction);
				GameObject newTile = Instantiate(_emptyGridPrefab) as GameObject;
				newTile.transform.parent = this.transform;
				newTile.transform.position = PositionForHexLocation(newLocation);
				_gameBoard.SetTileAt(newLocation, newTile);
				TileBehavior newTileBehavior = newTile.GetComponent<TileBehavior>();
				if (newTileBehavior != null) {
					newTileBehavior._TileData.location = newLocation;
				}
			}
		}
	}

	void SetCurrentHex(GameObject currentHex)
	{
		if (_currentHex != null)
		{
			TileBorderBlinkScript blink = _currentHex.GetComponent<TileBorderBlinkScript>();
			if (blink != null)
			{
				Destroy(blink);
			}
		}

		_currentHex = currentHex;

		if (_currentHex != null)
		{
			_currentHex.AddComponent(typeof(TileBorderBlinkScript));

			TileBehavior tileBehavior = _currentHex.GetComponent<TileBehavior>();
			if (tileBehavior != null)
			{
				GenerateBoardAroundTile(tileBehavior._TileData.location);
			}
		}
	}

	Vector3 CalculateGridOffset(Vector3 startingOffset, BOARD_DIRECTION direction)
	{
		switch (direction)
		{
		case BOARD_DIRECTION.NORTH:
			startingOffset.z += 2*Z_OFFSET;
			break;
		case BOARD_DIRECTION.NORTH_EAST:
			startingOffset.x += X_OFFSET;
			startingOffset.z += Z_OFFSET;
			break;
		case BOARD_DIRECTION.NORTH_WEST:
			startingOffset.x -= X_OFFSET;
			startingOffset.z += Z_OFFSET;
			break;
		case BOARD_DIRECTION.SOUTH:
			startingOffset.z -= 2*Z_OFFSET;
			break;
		case BOARD_DIRECTION.SOUTH_EAST:
			startingOffset.x += X_OFFSET;
			startingOffset.z -= Z_OFFSET;
			break;
		case BOARD_DIRECTION.SOUTH_WEST:
			startingOffset.x -= X_OFFSET;
			startingOffset.z -= Z_OFFSET;
			break;
		}
		return startingOffset;
	}

	Vector3 PositionForHexLocation(HexLocation location) {
		Vector3 pos = new Vector3();
		pos.x = X_OFFSET * location.x;
		pos.z = 2 * Z_OFFSET * location.z;
		if (location.x % 2 != 0) 
		{
			pos.z += Z_OFFSET;
		}

		return pos;
	}
}

