using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    public List<PhotoRenderer> cameras;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var camera in cameras)
            {
                camera.ChangeBool();
            }
        }
    }
}
