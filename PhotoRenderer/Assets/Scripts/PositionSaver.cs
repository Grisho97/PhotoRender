using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PositionSaver : MonoBehaviour
{
    public string fileName;
    public Transform Model;

    [SerializeField]
    private TransformData Transforms;

    private void OnEnable()
    {
        Transforms = new TransformData {ModelTransformsList = new List<ModelTransforms>()};
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetPosition();
        }
        

        if (Input.GetKeyDown(KeyCode.A))
        {
            SaveFile();
        }
    }

    private void GetPosition()
    {
        ModelTransforms modelTransforms = new ModelTransforms
        {
            modelPosition = Model.position,
            modelRotation = Model.eulerAngles
        };
        Transforms.ModelTransformsList.Add(modelTransforms);
        Debug.Log("Model position added. Move model to a new place or press A to save file");
    }

    private void SaveFile()
    {
        File.WriteAllText(Application.dataPath + "/Resources/ModelPositions/"+ fileName + ".json", JsonUtility.ToJson(Transforms));
        Debug.Log("ModelPosition file saved");
    }
}

[Serializable]
public struct ModelTransforms
{
    public Vector3 modelPosition;
    public Vector3 modelRotation;
}

[Serializable]
public class TransformData
{
    public List<ModelTransforms> ModelTransformsList;
}
