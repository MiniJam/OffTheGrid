using UnityEngine;
using System.Collections;

public class TileBehavior : MonoBehaviour
{
	public GameTile _TileData;
	public Renderer borderRenderer;

	void Awake()
	{
		_TileData = new GameTile();
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
