using UnityEngine;
using System.Collections;

public class MainMenuBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Update () 
	{
		if (Input.GetMouseButtonUp (0))
		{
			Ray touchTestRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(touchTestRay, out hitInfo))
			{
				//GameObject touchedObject = hitInfo.collider.gameObject;

			}
		}
	}
}
