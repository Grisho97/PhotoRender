using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraPositionSaver : MonoBehaviour
{
    public Transform Model;
    public string fileName;
    
    [SerializeField]
    private TransformData Transforms;

    private void OnEnable()
    {
        Transforms = new TransformData {ModelTransformsList = new List<ModelTransforms>()};
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
        ModelTransforms modelTransforms = new ModelTransforms
        {
            modelPosition = Model.position,
            modelRotation = Model.rotation.eulerAngles
        };
        Transforms.ModelTransformsList.Add(modelTransforms);

    }

    private void SaveFile()
    {
        File.WriteAllText(Application.dataPath + "/Resources/CameraParameters/"+ fileName + ".json", JsonUtility.ToJson(Transforms));
    }
}

[Serializable]
public struct CameraParameters
{
    
}
