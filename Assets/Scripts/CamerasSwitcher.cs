using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasSwitcher : MonoBehaviour
{
    [SerializeField] private Camera[] controllableCameras;

    public void SwitchCamera(List<Camera> targetCamera)
    {
        foreach (var camera in controllableCameras)
        {
            camera.gameObject.SetActive(targetCamera.Contains(camera));
        }
    }
}
