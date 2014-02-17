using UnityEngine;
using System.Collections;

public class TileBehavior : MonoBehaviour
{
	public GameTile _TileData;
	public Renderer borderRenderer;
	public GameObject _visualization;

	public bool revealed {
		get { return _TileData.revealed; }
	}

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

	public void RevealTile(GameObject newVis)
	{
		if (_TileData.revealed) {
			return;
		}

		_TileData.revealed = true;
		Destroy(_visualization);

		_visualization = newVis;
		newVis.transform.parent = this.transform;
		newVis.transform.localPosition = Vector3.zero;
	}
}
