using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStrategy
{
    public Transform CameraTransform;

    public Transform CameraStartTransform;
    public Transform TargetTransform;
    public int AmmountofPhotos;

    public List<Transform> cameraPositions;
    

    private void Rotate360()
    {
        CameraTransform = CameraStartTransform;
        
        if( AmmountofPhotos == 0 )
            return;
        
        for (int i = 0; i < AmmountofPhotos; i++)
        {
            cameraPositions.Add(CameraTransform);
            CameraTransform.RotateAround(TargetTransform.position, Vector3.up, 360/AmmountofPhotos);
        }
        
    }

    public List<Transform> GetCameraPositions(CameraParameters cameraParameters)
    {
        CameraStartTransform.position = cameraParameters.CameraStartPosition;
        CameraStartTransform.eulerAngles = cameraParameters.CameraStartRotation;
        TargetTransform.position = cameraParameters.TargetPosition;
        AmmountofPhotos = cameraParameters.AmmountofPhotos;
        Rotate360();
        return cameraPositions;
    }
}
