﻿using UnityEngine;
using System.Collections;

public class TileBorderBlinkScript : MonoBehaviour
{
	public Renderer _borderRenderer;
	public Color _currentColor;
	private bool _isAlphaShrinking = true;
	private const float MIN_ALPHA = 0.50f;
	private const float MAX_ALPHA = 1.0f;
	private const float ALPHA_CHANGE = 0.01f;
	
	void Awake()
	{
		_borderRenderer = GetComponent<TileBehavior>().borderRenderer;
		_currentColor = _borderRenderer.material.color;
	}

	// Use this for initialization
	void Start ()
	{
	}

	void OnDestroy()
	{
		_currentColor.a = MAX_ALPHA;
		_borderRenderer.material.color = _currentColor;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float currentAlpha = _currentColor.a;
		
		if (_isAlphaShrinking)
		{
			if ((currentAlpha - ALPHA_CHANGE) < MIN_ALPHA)
			{
				_isAlphaShrinking = false;
			}
			else
			{
				_currentColor.a -= ALPHA_CHANGE;
			}
		}
		else
		{
			if ((currentAlpha + ALPHA_CHANGE) > MAX_ALPHA)
			{
				_isAlphaShrinking = true;
			}
			else
			{
				_currentColor.a += ALPHA_CHANGE;
			}
		}
		
		_borderRenderer.material.color = _currentColor;
	}
}
