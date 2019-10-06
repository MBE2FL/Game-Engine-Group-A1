using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 1.0f;
    private Rigidbody _rb;
    //[SerializeField]
    //private Vector3 _camOffset;
    //private Camera _cam = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        //_cam = Camera.main;
        //Quaternion rot = new Quaternion();
        //rot.eulerAngles = new Vector3(50.0f, 0.0f, 0.0f);
        //_cam.transform.rotation = rot;
    }

    // Update is called once per frame
    void Update()
    {
        // Move with WASD
        //if (Input.GetKey(KeyCode.W))
        //    transform.position += transform.forward * _movementSpeed;
        //else if (Input.GetKey(KeyCode.S))
        //    transform.position -= transform.forward * _movementSpeed;
        //else if (Input.GetKey(KeyCode.A))
        //    transform.position -= transform.right * _movementSpeed;
        //else if (Input.GetKey(KeyCode.D))
        //    transform.position += transform.right * _movementSpeed;

        if (Input.GetKey(KeyCode.W))
            _rb.AddForce(transform.forward * _movementSpeed);
        else if (Input.GetKey(KeyCode.S))
            _rb.AddForce(-transform.forward * _movementSpeed);
        else if (Input.GetKey(KeyCode.A))
            _rb.AddForce(-transform.right * _movementSpeed);
        else if (Input.GetKey(KeyCode.D))
            _rb.AddForce(transform.right * _movementSpeed);

        // Camera follow player
        //_cam.transform.position = transform.position + _camOffset;
    }
}
