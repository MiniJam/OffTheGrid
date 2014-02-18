using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public GestureDetector _gestureDetector;
	public Transform target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distanceMin = .5f;
	public float distanceMax = 15f;

	float x = 0.0f;
	float y = 0.0f;

	void Awake()
	{
		_gestureDetector = new GestureDetector();
	}

	void Start()
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		// Make the rigid body not change rotation
		if (rigidbody)
		{
			rigidbody.freezeRotation = true;
		}
	}

	void Update()
	{
		_gestureDetector.Update();

		if (Input.GetKey(KeyCode.LeftArrow) ||
			Input.GetKey(KeyCode.RightArrow) ||
			Input.GetKey(KeyCode.UpArrow) ||
			Input.GetKey(KeyCode.DownArrow))
		{
			float horizontalAxis = Input.GetAxis("Horizontal");
			float verticalAxis = Input.GetAxis("Vertical");

			Vector3 position = Camera.main.transform.position;
			position.x = Mathf.Clamp(position.x + horizontalAxis * 0.5f, -10, 10);
			position.z = Mathf.Clamp(position.z + verticalAxis * 0.5f, -10, 10);
			Camera.main.transform.position = position;
		}

		if (Camera.main.isOrthoGraphic)
		{
			float orthoSize = Camera.main.orthographicSize;
			orthoSize -= Input.GetAxis("Mouse ScrollWheel");
			orthoSize = Mathf.Clamp(orthoSize, 2, 10);
			Camera.main.orthographicSize = orthoSize;
		}

		if ((target) && (_gestureDetector.IsDrag()))
		{
			x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

			y = ClampAngle(y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler(y, x, 0);

			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

			RaycastHit hit;
			if (Physics.Linecast(target.position, transform.position, out hit))
			{
				distance -= hit.distance;
			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;

			transform.rotation = rotation;
			transform.position = position;
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
		{
			angle += 360F;
		}

		if (angle > 360F)
		{
			angle -= 360F;
		}

		return Mathf.Clamp(angle, min, max);
	}
}
