using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour 
{
	private const float X_OFFSET = 3.5f;
	private const float Z_OFFSET = 2.0f;

	public enum BOARD_DIRECTION
	{
		DIRECTION_NOT_SET = 0,
		NORTH = 1,
		NORTH_EAST = 2,
		SOUTH_EAST = 3,
		SOUTH = 4,
		SOUTH_WEST = 5,
		NORTH_WEST = 6,
		MAX_DIRECTIONS
	}

	public GameObject _emptyGridPrefab;
	public List<GameObject> _tiles = new List<GameObject>();
	public GameObject _currentHex;

	void Awake()
	{

	}

	// Use this for initialization
	void Start ()
	{
		// generate empty tiles 5 deep around the starting location
		GameObject initialTile = Instantiate (_emptyGridPrefab) as GameObject;
		_tiles.Add (initialTile);
		initialTile.transform.parent = this.transform;
		SetCurrentHex (initialTile);

		GenerateBoardAroundTile(initialTile);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonUp (0))
		{
			Ray touchTestRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(touchTestRay, out hitInfo))
			{
				GameObject touchedObject = hitInfo.collider.gameObject;
				SetCurrentHex(touchedObject);
			}
		}
	}

	void GenerateBoardAroundTile(GameObject tile)
	{
		Vector3 initialPosition = tile.transform.position;
		for (BOARD_DIRECTION direction = BOARD_DIRECTION.NORTH;
		     direction != BOARD_DIRECTION.MAX_DIRECTIONS;
	     	 ++direction)
		{
			Vector3 newPosition = CalculateGridOffset (initialPosition, direction);

			//TODO check if tile exists
			GameObject newTile = Instantiate (_emptyGridPrefab) as GameObject;
			newTile.transform.position = newPosition;
			newTile.transform.parent = this.transform;
		}
	}

	void SetCurrentHex(GameObject currentHex)
	{
		if (_currentHex != null)
		{
			Component blink = _currentHex.GetComponent(typeof(AlphaBlinkScript));
			if (blink != null)
			{
				Destroy(blink);
			}
		}

		_currentHex = currentHex;

		if (_currentHex != null)
		{
			_currentHex.AddComponent(typeof(AlphaBlinkScript));
			GenerateBoardAroundTile (_currentHex);
		}
	}

	Vector3 CalculateGridOffset(Vector3 startingOffset, BoardManager.BOARD_DIRECTION direction)
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
}
