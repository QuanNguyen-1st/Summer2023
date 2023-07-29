using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera _vcam;
    private Transform _transform;
    [SerializeField] private GameObject _player;
    [SerializeField] private KeyCode _keyCodeLockCam;
    // Start is called before the first frame update
    private void Awake() {
        _player = FindObjectOfType<PlayerMovement>().gameObject;
    }
    void Start()
    {
        // _pm = FindObjectOfType<PlayerMovement>();
        _vcam = GetComponent<CinemachineVirtualCamera>();
        _transform = GetComponent<Transform>();
        _vcam.Follow = _player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CamControl();
        CheckCam();
    }

    private void CamControl()
    {
        if (Input.GetKeyDown(_keyCodeLockCam))
        {
            if (_vcam.Follow == _player.transform)
            {
                _vcam.Follow = null;
                _transform.localPosition = Vector3.zero;

            }
            else if (_vcam.Follow == null)
            {
                _vcam.Follow = _player.transform;
            }
        }
    }

    private void CheckCam() {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(_player.transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            // Your object is in the range of the camera, you can apply your behaviour
            
        }
        else {
            _vcam.Follow = _player.transform;
        }
            
    }
}
