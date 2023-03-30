using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    private void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + targetCamera.transform.forward);
    }
}
