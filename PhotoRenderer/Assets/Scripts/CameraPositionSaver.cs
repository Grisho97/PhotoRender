using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraPositionSaver : MonoBehaviour
{
    public Transform CameraTransform;
    public Transform TargetTransform;
    public int AmmountOfPhotos;
    public string fileName;
    
    [SerializeField]
    private CameraParameters cameraParameters;

    private void OnEnable()
    {
        cameraParameters = new CameraParameters();
        CameraTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetPosition();
        }
        

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveFile();
        }
    }

    private void GetPosition()
    {
        TargetTransform = GetComponent<Transform>();
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
public struct CameraParameters
{
    public Vector3 CameraStartPosition;
    public Vector3 CameraStartRotation;
    public Vector3 TargetPosition;
    public int AmmountofPhotos;
}
