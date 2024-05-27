using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;

    private CinemachineVirtualCamera _currentCamera;


  

    private void Start()
    {
        _currentCamera = _mainVirtualCamera;
    }
    public void SetNewCamera(CinemachineVirtualCamera newCam)
    {
        if (_currentCamera)
            _currentCamera.Priority = 0;

        _currentCamera = newCam;
        newCam.Priority = 1;
    }

    public void ResetToMainCamera()
    {
        SetNewCamera(_mainVirtualCamera);
    }

}
