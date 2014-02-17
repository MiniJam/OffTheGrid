using UnityEngine;
using System.Collections;

public class MainMenuBehavior : MonoBehaviour 
{
	void Start ()
	{
	}

	void Update () 
	{
	}

	void OnGUI()
	{
		// Make a background box
		float borderWidth = Screen.width * 0.1f;
		float menuWidth = Screen.width - 2 * borderWidth;
		float menuHeight = Screen.height - 3 * borderWidth;

		GUIStyle titleStyle = GUI.skin.box;
		titleStyle.fontSize = 20;

		GUI.Box(new Rect(borderWidth, borderWidth, menuWidth, menuHeight), "Off the Grid", titleStyle);

		float optionLocation = borderWidth * 3;
		float optionWidth = Screen.width - 2 * optionLocation;

		if (GUI.Button(new Rect(optionLocation, borderWidth + 40, optionWidth, 20), "Start New Game"))
		{
			Application.LoadLevel("MainGame");
		}
	}
}
