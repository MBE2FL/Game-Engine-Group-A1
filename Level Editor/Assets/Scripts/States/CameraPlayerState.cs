using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerState : MonoBehaviour, ICameraState
{
    Camera _cam;
    Transform _playerTrans = null;
    float _angle = 0.0f;

    private void Start()
    {
        _cam = Camera.main;
    }

    public void entry(CameraControl camControl)
    {
        Quaternion rot = new Quaternion();
        rot.eulerAngles = new Vector3(35.0f, 0.0f, 0.0f);
        _cam.transform.rotation = rot;

        _playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        _cam.transform.position = _playerTrans.position + camControl.CamOffset;
    }

    public ICameraState input(CameraControl camControl)
    {
        // Remain in current state.
        return null;
    }

    public void update(CameraControl camControl)
    {
        // Camera follow player
        float horizontal = Input.GetAxis("Mouse X");
        //float vertical = Input.GetAxis("Mouse Y");


        _playerTrans.transform.Rotate(new Vector3(0.0f, horizontal * 2.0f, 0.0f));


        //_cam.transform.RotateAround(_playerTrans.position, Vector3.up, horizontal);
        _angle += horizontal;


        _cam.transform.position = _playerTrans.position + Quaternion.Euler(0.0f, _angle, 0.0f) * camControl.CamOffset;
        _cam.transform.LookAt(_playerTrans);


        if (EnemySpawner.AllEnemiesDead)
        {
            camControl.stop();
            Debug.Log("Game Over");
        }
    }
}
