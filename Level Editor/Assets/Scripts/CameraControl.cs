using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    static CameraMoveState _moveState = null;
    static CameraObjectSelectedState _objectSelectedState = null;
    static CameraPlayerState _playerState = null;
    private GameObject _selectedObj;
    private ICameraState _state = null;
    [SerializeField]
    private Vector3 _camOffset = new Vector3(0.0f, 2.5f, -2.5f);
    private GameObject _player;

    public GameObject SelectedObj
    {
        get
        {
            return _selectedObj;
        }
        set
        {
            _selectedObj = value;
        }
    }

    public static CameraMoveState MoveState
    {
        get
        {
            return _moveState;
        }
    }

    public static CameraObjectSelectedState ObjectSelectedState
    {
        get
        {
            return _objectSelectedState;
        }
    }

    public Vector3 CamOffset
    {
        get
        {
            return _camOffset;
        }
    }


    private void Awake()
    {
        _moveState = gameObject.AddComponent<CameraMoveState>();
        _objectSelectedState = gameObject.AddComponent<CameraObjectSelectedState>();
        _playerState = gameObject.AddComponent<CameraPlayerState>();
        _state = _moveState;
    }


    // Update is called once per frame
    void Update()
    {
        ICameraState state = _state.input(this);

        if (state == null)
        {
            _state.update(this);
        }
        else
        {
            _state = state;
            _state.entry(this);
        }
        

        #region
        //bool leftClicked = Input.GetMouseButtonDown(0);
        //bool rightClicked = Input.GetMouseButtonDown(1);


        //// Select an object when left clicked, and there is no currently selected object.
        //if (leftClicked && !_selectedObj && !EventSystem.current.IsPointerOverGameObject())
        //{
        //    Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit rayHit;

        //    if (Physics.Raycast(mousePos.origin, mousePos.direction, out rayHit, Mathf.Infinity))
        //    {
        //        _selectedObj = rayHit.collider.gameObject;
        //        Debug.Log("Selected: " + _selectedObj.name);
        //    }
        //}
        //// Deselect currently selected object
        //else if (rightClicked)
        //    _selectedObj = null;



        //float horizontal = Input.GetAxis("Mouse X");
        //float vertical = Input.GetAxis("Mouse Y");
        //// Move selected object with mouse
        //if (_selectedObj)
        //{
        //    float height = 0.0f;

        //    if (Input.GetMouseButton(2))
        //    {
        //        height = vertical;
        //        horizontal = 0.0f;
        //        vertical = 0.0f;
        //    }

        //    if (Input.GetMouseButton(0))
        //        _selectedObj.transform.position += new Vector3(horizontal, height, vertical);
        //}
        //// Move camera
        //else
        //{
        //    if (Input.GetMouseButton(1))
        //    {
        //        transform.position += transform.right * horizontal;
        //        transform.position += transform.up * vertical;
        //    }

        //    transform.position += transform.forward * Input.GetAxis("Mouse ScrollWheel") * 3.0f;

        //    if (Input.GetMouseButton(2))
        //    {
        //        transform.Rotate(new Vector3(2.0f * vertical, 0.0f, 0.0f));
        //        transform.Rotate(new Vector3(0.0f, 2.0f * horizontal, 0.0f));
        //    }
        //}
        #endregion
    }

    public void play()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;

        Factory.Instance.CreateGameObject(ObjectTypes.Player, out _player);

        _state = _playerState;
        _state.entry(this);
    }

    public void stop()
    {
        if (_player)
        {
            Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            _state = _moveState;
            _state.entry(this);

            Factory.Instance.DeleteGameObject(ref _player);
        }
    }
}
