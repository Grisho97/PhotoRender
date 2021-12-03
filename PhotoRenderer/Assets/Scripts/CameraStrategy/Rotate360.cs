using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rotate360 : ICameraStrategy
{
    private Transform CameraTransform;
    private Vector3 CameraStartPosition;
    private Vector3 CameraStartRotation;
    private Vector3 TargetTransform;
    public int AmmountofPhotos;
    
    private List<CameraTransformData> cameraPositions;

    private CameraParametersRotate360 cameraParametersRotate360;
    

    private void Rotate()
    {
        CameraTransform.position = CameraStartPosition;
        CameraTransform.eulerAngles = CameraStartRotation;
        
        if( AmmountofPhotos == 0 )
            return;
        
        for (int i = 0; i < AmmountofPhotos; i++)
        {
            CameraTransformData data = new CameraTransformData
            {
                cameraPosition = CameraTransform.position, 
                cameraRotation = CameraTransform.eulerAngles
            };
            cameraPositions.Add(data);
            CameraTransform.RotateAround(TargetTransform, Vector3.up, 360/AmmountofPhotos);
        }
        
    }

    public List<CameraTransformData> GetCameraPositions(CameraParameters cameraParameters, Transform cameraTransform)
    {
        CameraTransform = cameraTransform;
        cameraPositions = new List<CameraTransformData>();
        cameraParametersRotate360 = cameraParameters as CameraParametersRotate360;
        
        CameraStartPosition = cameraParametersRotate360.CameraStartPosition;
        CameraStartRotation = cameraParametersRotate360.CameraStartRotation;
        TargetTransform = cameraParametersRotate360.TargetPosition;
        AmmountofPhotos = cameraParametersRotate360.AmmountofPhotos;
        Rotate();
        return cameraPositions;
    }

    public CameraParameters GetCameraParameters(CameraParameters cameraParameters)
    {
        cameraParametersRotate360 = new CameraParametersRotate360
        {
            CameraSrategyName = cameraParameters.CameraSrategyName,
            CameraStartPosition = cameraParameters.CameraStartPosition,
            CameraStartRotation = cameraParameters.CameraStartRotation,
            TargetPosition = cameraParameters.TargetPosition,
            AmmountofPhotos = AmmountofPhotos
        };
        return cameraParametersRotate360;
    }
}

[Serializable]
public class CameraParametersRotate360 : CameraParameters
{
    public int AmmountofPhotos;
}
