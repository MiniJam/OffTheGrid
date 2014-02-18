using UnityEngine;
using System.Collections;

public class GameMenuGUI : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI()
	{
		float actionMenuWidth = 75;
		float actionMenuTop = 0;
		float actionMenuLeft = Screen.width - actionMenuWidth;

		float optionTop = 25;
		float optionLeft = actionMenuLeft + 5;
		float optionWidth = actionMenuWidth - 10;
		float optionHeight = 20;
		float optionSpacing = 5;

		if (GUI.Button(new Rect(optionLeft, optionTop, optionWidth, optionHeight), "Move"))
		{
		}

		optionTop += optionHeight + optionSpacing;

		if (GUI.Button(new Rect(optionLeft, optionTop, optionWidth, optionHeight), "Secure"))
		{
		}

		optionTop += optionHeight + optionSpacing;

		if (GUI.Button(new Rect(optionLeft, optionTop, optionWidth, optionHeight), "Search"))
		{
		}

		float actionMenuHeight = optionTop + optionHeight + optionSpacing;
		GUI.Box(new Rect(actionMenuLeft, actionMenuTop, actionMenuWidth, actionMenuHeight), "Actions");
	}
}
