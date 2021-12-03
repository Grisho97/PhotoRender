using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SecurityCamera : ICameraStrategy
{
    private Transform CameraTransform;
    private Vector3 CameraStartPosition;
    private Vector3 CameraStartRotation;
    private Vector3 TargetTransform;
    public float Angle;
    public int AmmountOfSteps;
    
    private List<CameraTransformData> cameraPositions;

    private CameraParametersSecurityCamera cameraParametersSecurityCamera;

    public void Scan()
    {
        CameraTransform.position = CameraStartPosition;
        CameraTransform.eulerAngles = CameraStartRotation;
        
        if( AmmountOfSteps <= 0 )
            return;

        CameraTransformData data = new CameraTransformData
        {
            cameraPosition = CameraTransform.position, 
            cameraRotation = CameraTransform.eulerAngles
        };
        cameraPositions.Add(data);
        
        for (int i = 1; i < AmmountOfSteps+1; i++)
        {
            CameraTransform.eulerAngles = CameraStartRotation + new Vector3(0, Angle*i,0);
            data = new CameraTransformData
            {
                cameraPosition = CameraTransform.position, 
                cameraRotation = CameraTransform.eulerAngles
            };
            cameraPositions.Add(data);
            
            CameraTransform.eulerAngles = CameraStartRotation + new Vector3(0, -Angle*i,0);
            data = new CameraTransformData
            {
                cameraPosition = CameraTransform.position, 
                cameraRotation = CameraTransform.eulerAngles
            };
            cameraPositions.Add(data);
            
            CameraTransform.eulerAngles = CameraStartRotation + new Vector3( Angle*i,0,0);
            data = new CameraTransformData
            {
                cameraPosition = CameraTransform.position, 
                cameraRotation = CameraTransform.eulerAngles
            };
            cameraPositions.Add(data);
            
            CameraTransform.eulerAngles = CameraStartRotation + new Vector3(-Angle*i,0,0); 
            data = new CameraTransformData
            {
                cameraPosition = CameraTransform.position, 
                cameraRotation = CameraTransform.eulerAngles
            };
            cameraPositions.Add(data);
        }
    }
    
    public List<CameraTransformData> GetCameraPositions(CameraParameters cameraParameters, Transform cameraTransform)
    {
        CameraTransform = cameraTransform;
        cameraPositions = new List<CameraTransformData>();
        cameraParametersSecurityCamera = cameraParameters as CameraParametersSecurityCamera;
        
        CameraStartPosition = cameraParametersSecurityCamera.CameraStartPosition;
        CameraStartRotation = cameraParametersSecurityCamera.CameraStartRotation;
        TargetTransform = cameraParametersSecurityCamera.TargetPosition;
        AmmountOfSteps = cameraParametersSecurityCamera.AmmountOfSteps;
        Angle = cameraParametersSecurityCamera.Angle;
        Scan();
        return cameraPositions;
    }

    public CameraParameters GetCameraParameters(CameraParameters cameraParameters)
    {
        cameraParametersSecurityCamera = new CameraParametersSecurityCamera()
        {
            CameraSrategyName = cameraParameters.CameraSrategyName,
            CameraStartPosition = cameraParameters.CameraStartPosition,
            CameraStartRotation = cameraParameters.CameraStartRotation,
            TargetPosition = cameraParameters.TargetPosition,
            AmmountOfSteps = AmmountOfSteps,
            Angle = Angle
        };
        return cameraParametersSecurityCamera;
    }
    
    
    [Serializable]
    public class CameraParametersSecurityCamera : CameraParameters
    {
        public float Angle;
        public int AmmountOfSteps;
    }
}
