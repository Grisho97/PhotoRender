using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WallMove : ICameraStrategy
{
    private Transform CameraTransform;
    private Vector3 CameraStartPosition;
    private Vector3 CameraStartRotation;
    private Vector3 TargetTransform;
    public float StepLength;
    public int AmmountOfSteps;
    
    private List<CameraTransformData> cameraPositions;

    private CameraParametersWallMove cameraParametersWallMove;
    

    private void Move()
    {
        CameraTransform.position = CameraStartPosition;
        CameraTransform.eulerAngles = CameraStartRotation;
        
        if( AmmountOfSteps <= 0 )
            return;
        
        for (int i = 0; i < AmmountOfSteps; i++)
        {
            var position = CameraTransform.position;
            CameraTransformData data = new CameraTransformData
            {
                cameraPosition = position, 
                cameraRotation = CameraTransform.eulerAngles
            };
            cameraPositions.Add(data);
            position.y -= StepLength;
            CameraTransform.position = position;
        }
        
    }

    public List<CameraTransformData> GetCameraPositions(CameraParameters cameraParameters, Transform cameraTransform)
    {
        CameraTransform = cameraTransform;
        cameraPositions = new List<CameraTransformData>();
        cameraParametersWallMove = cameraParameters as CameraParametersWallMove;
        
        CameraStartPosition = cameraParametersWallMove.CameraStartPosition;
        CameraStartRotation = cameraParametersWallMove.CameraStartRotation;
        TargetTransform = cameraParametersWallMove.TargetPosition;
        AmmountOfSteps = cameraParametersWallMove.AmmountOfSteps;
        StepLength = cameraParametersWallMove.StepLength;
        Move();
        return cameraPositions;
    }

    public CameraParameters GetCameraParameters(CameraParameters cameraParameters)
    {
        cameraParametersWallMove = new CameraParametersWallMove()
        {
            CameraSrategyName = cameraParameters.CameraSrategyName,
            CameraStartPosition = cameraParameters.CameraStartPosition,
            CameraStartRotation = cameraParameters.CameraStartRotation,
            TargetPosition = cameraParameters.TargetPosition,
            AmmountOfSteps = AmmountOfSteps,
            StepLength = StepLength
        };
        return cameraParametersWallMove;
    }
}

[Serializable]
public class CameraParametersWallMove : CameraParameters
{
    public float StepLength;
    public int AmmountOfSteps;
}
