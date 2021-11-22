using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PositionSaver : MonoBehaviour
{
    public Transform Model;
    public string fileName;
    
    [SerializeField]
    private ModelTransforms Transforms;

    private void OnEnable()
    {
        Transforms = new ModelTransforms {modelPosition = new List<Vector3>(), modelRotation = new List<Vector3>()};
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
        Transforms.modelPosition.Add(Model.position);
        Transforms.modelRotation.Add(Model.rotation.eulerAngles);
    }

    private void SaveFile()
    {
        File.WriteAllText(Application.dataPath + "/Resources/ModelPositions/"+ fileName + ".json", JsonUtility.ToJson(Transforms));
    }
}

[Serializable]
public class ModelTransforms
{
    public List<Vector3> modelPosition;
    public List<Vector3> modelRotation;
}
