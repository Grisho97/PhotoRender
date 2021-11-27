using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

public class CameraPositionSaver : MonoBehaviour
{
    public enum CameraStrategyName
    {
        Rotate360,
        WallMove,
        SecurityCamera
    }

    public CameraStrategyName cameraStrategyName;

    [SerializeField] private Rotate360 rotate360;
    [SerializeField] private WallMove wallMove;
    [SerializeField] private SecurityCamera securityCamera;
    
    public Transform CameraTransform;
    public Transform TargetTransform;
    public string fileName;
    
    [SerializeField]
    private CameraParameters cameraParameters;

    private ICameraStrategy CameraStrategy;

    private void OnEnable()
    {
        cameraParameters = new CameraParameters();
        CameraTransform = GetComponent<Transform>();
    }

    void Update()
    {
        CameraTransform.LookAt(TargetTransform);
        
        if (Input.GetKey(KeyCode.E))
        {
            GetParams();
        }
        

        if (Input.GetKey(KeyCode.S))
        {
            SaveFile();
        }
    }

    private void GetParams()
    {
        switch (cameraStrategyName)
        {
            case CameraStrategyName.Rotate360:
                CameraStrategy = rotate360;
                break;
            case CameraStrategyName.WallMove:
                CameraStrategy = wallMove;
                break;
            case CameraStrategyName.SecurityCamera:
                CameraStrategy = securityCamera;
                break;
        }
        cameraParameters.CameraSrategyName = cameraStrategyName.ToString();
        cameraParameters.CameraStartPosition = CameraTransform.position;
        cameraParameters.CameraStartRotation = CameraTransform.eulerAngles;
        cameraParameters.TargetPosition = TargetTransform.position;
        cameraParameters = CameraStrategy.GetCameraParameters(cameraParameters);
    }

    private void SaveFile()
    {
        File.WriteAllText(Application.dataPath + "/Resources/CameraParameters/"+ fileName + ".json", JsonUtility.ToJson(cameraParameters));
    }
    
    
}

[Serializable]
public class CameraParameters
{
    public string CameraSrategyName;
    public Vector3 CameraStartPosition;
    public Vector3 CameraStartRotation;
    public Vector3 TargetPosition;
}

