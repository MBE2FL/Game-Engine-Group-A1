using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveState : MonoBehaviour, ICameraState
{
    public void entry(CameraControl camControl)
    {
        
    }

    public ICameraState input(CameraControl camControl)
    {
        // Left click checks to see if you clicked on an object.
        if (Input.GetMouseButtonDown(0))
        {
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(mousePos.origin, mousePos.direction, out rayHit, Mathf.Infinity))
            {
                camControl.SelectedObj = rayHit.collider.gameObject;
                Debug.Log("Selected: " + camControl.SelectedObj.name);

                return CameraControl.ObjectSelectedState;
            }
        }

        return null;
    }
    public void update(CameraControl camControl)
    {
        // Move the camera around.
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");
        Transform transform = camControl.GetComponent<Transform>();

        // Right click moves the camera left/right, up/down.
        if (Input.GetMouseButton(1))
        {
            transform.position += transform.right * horizontal;
            transform.position += transform.up * vertical;
        }

        // Moving the scroll wheel moves the camera along the world z-axis.
        transform.position += transform.forward * Input.GetAxis("Mouse ScrollWheel") * 3.0f;

        // Middle mouse button rotates the camera.
        if (Input.GetMouseButton(2))
        {
            transform.Rotate(new Vector3(2.0f * vertical, 0.0f, 0.0f));
            transform.Rotate(new Vector3(0.0f, 2.0f * horizontal, 0.0f));
        }
    }
}
