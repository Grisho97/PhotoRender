using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public CameraStrategy CameraStrategy;
    
    public Transform CameraTransform;
    public Transform TargetTransform;
    public int AmmountOfPhotos;
    public string fileName;
    
    [SerializeField]
    private CameraParameters cameraParameters;

    private CameraStrategyName oldName;

    private void OnEnable()
    {
        cameraParameters = new CameraParameters();
        CameraTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (oldName != cameraStrategyName)
        {
             
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            GetPosition();
        }
        

        if (Input.GetKey(KeyCode.S))
        {
            SaveFile();
        }
    }

    private void GetPosition()
    {
        cameraParameters.CameraStartPosition = CameraTransform.position;
        cameraParameters.CameraStartRotation = CameraTransform.eulerAngles;
        cameraParameters.TargetPosition = TargetTransform.position;
        cameraParameters.AmmountofPhotos = AmmountOfPhotos;

    }

    private void SaveFile()
    {
        File.WriteAllText(Application.dataPath + "/Resources/CameraParameters/"+ fileName + ".json", JsonUtility.ToJson(cameraParameters));
    }
    
    
}

[Serializable]
public class CameraParameters
{
    public Vector3 CameraStartPosition;
    public Vector3 CameraStartRotation;
    public Vector3 TargetPosition;
    public int AmmountofPhotos;
}
