using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectSelectedState : MonoBehaviour, ICameraState
{
    public void entry(CameraControl camControl)
    {
 
    }

    public ICameraState input(CameraControl camControl)
    {
        // Deselect currently selected object, and switch states.
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("De-selected: " + camControl.SelectedObj.name);
            camControl.SelectedObj = null;
            return CameraControl.MoveState;
        }

        // Remain in current state.
        return null;
    }

    public void update(CameraControl camControl)
    {
        // Move selected object with mouse
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");
        float height = 0.0f;

        // Middle mouse button moves the selected object along the world y-axis.
        if (Input.GetMouseButton(2))
        {
            height = vertical;
            horizontal = 0.0f;
            vertical = 0.0f;
        }

        // Left mouse button moves the selected object along the world x, and z axis.
        if (Input.GetMouseButton(0))
            camControl.SelectedObj.transform.position += new Vector3(horizontal, height, vertical);
    }
}
