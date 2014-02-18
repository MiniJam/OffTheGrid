using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestureDetector
{
	private const float CLICK_THRESHOLD = 10.0f;

	public Vector3 _clickStart;
	public Vector3 _clickStop;
	public Vector3 _dragLocation;
	public bool _clickStartSet = false;
	public bool _clickStopSet = false;
	public bool _isClicked = false;
	public bool _isDragging = false;

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_clickStart = Input.mousePosition;
			_clickStartSet = true;
			_clickStopSet = false;
			_isDragging = false;
		}

		if ((Input.GetMouseButton(0)) && (_clickStartSet))
		{
			_dragLocation = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0))
		{
			_clickStop = Input.mousePosition;
			_clickStopSet = true;
		}

		if (_clickStartSet && _clickStopSet)
		{
			_clickStartSet = false;
			_clickStopSet = false;

			float clickDistance = Vector3.Distance(_clickStart, _clickStop);
			if (clickDistance < CLICK_THRESHOLD)
			{
				_isClicked = true;
			}
		}
		else
		{
			_isClicked = false;
		}

		if ((_clickStartSet) && (!_clickStopSet))
		{
			float distance = Vector3.Distance(_clickStart, _dragLocation);
			if (distance > CLICK_THRESHOLD)
			{
				_isDragging = true;
			}
		}
		else
		{
			_isDragging = false;
		}
	}

	public bool IsClick()
	{
		return _isClicked;
	}

	public bool IsDrag()
	{
		return _isDragging;
	}

}
