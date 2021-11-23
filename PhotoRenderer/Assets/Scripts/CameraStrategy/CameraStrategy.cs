using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStrategy
{
    public Transform CameraTransform;
    public Vector3 CameraStartPosition;
    public Vector3 CameraStartRotation;
    public Vector3 TargetTransform;
    public int AmmountofPhotos;
    
    public List<CameraTransformData> cameraPositions;
    

    private void Rotate360()
    {
        CameraTransform.position = CameraStartPosition;
        CameraTransform.eulerAngles = CameraStartRotation;
        
        if( AmmountofPhotos == 0 )
            return;
        
        for (int i = 0; i < AmmountofPhotos; i++)
        {
            CameraTransformData data = new CameraTransformData();
            data.cameraPosition = CameraTransform.position;
            data.cameraRotation = CameraTransform.eulerAngles;
            cameraPositions.Add(data);
            CameraTransform.RotateAround(TargetTransform, Vector3.up, 360/AmmountofPhotos);
        }
        
    }

    public List<CameraTransformData> GetCameraPositions(CameraParameters cameraParameters, Transform cameraTransform)
    {
        CameraTransform = cameraTransform;
        cameraPositions = new List<CameraTransformData>();
        
        CameraStartPosition = cameraParameters.CameraStartPosition;
        CameraStartRotation = cameraParameters.CameraStartRotation;
        TargetTransform = cameraParameters.TargetPosition;
        AmmountofPhotos = cameraParameters.AmmountofPhotos;
        Rotate360();
        return cameraPositions;
    }
}

[Serializable]
public struct CameraTransformData
{
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
}
