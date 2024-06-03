using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRegister : MonoBehaviour
{
    private void OnEnable()
    {
        CameraManager.Register(GetComponent<Cinemachine.CinemachineVirtualCamera>());
    }
    private void OnDisable()
    {
        CameraManager.Unregister(GetComponent<Cinemachine.CinemachineVirtualCamera>());
    }
}
