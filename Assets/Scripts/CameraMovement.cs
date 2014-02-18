using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow))
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");
            if (Camera.current != null)
            {
                Vector3 position = Camera.current.transform.position;
				position.x = Mathf.Clamp(position.x + horizontalAxis * 0.5f, -10, 10);
				position.z = Mathf.Clamp(position.z + verticalAxis * 0.5f, -10, 10);
                Camera.current.transform.position = position;
            }
        }

        if (Camera.main.isOrthoGraphic)
        {
            float orthoSize = Camera.main.orthographicSize;
            orthoSize -= Input.GetAxis("Mouse ScrollWheel");
            orthoSize = Mathf.Clamp(orthoSize, 2, 10);
            Camera.main.orthographicSize = orthoSize;
        }
    }
}
