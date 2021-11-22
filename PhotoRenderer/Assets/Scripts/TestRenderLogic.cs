using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestRenderLogic : MonoBehaviour
{
    public List<GameObject> EnvironmentPrefabs;
    public List<GameObject> ModelPrefabs;
    public List<GameObject> LightPrefabs;

    public ModelTransforms ModelTransforms;

    public List<Transform> CameraTransforms;
    public CameraStrategy CameraStrategy;

    private CamerasController camerasController;
    
    

    private void Start()
    {
        BuildAllPrefabs();
        
        ModelTransforms =
            JsonUtility.FromJson<ModelTransforms>(
                File.ReadAllText(Application.dataPath + "/Resources/ModelPositions/1.json"));

        CameraTransforms = CameraStrategy.GetCameraPositions();
    }

    private void BuildAllPrefabs()
    {
        
    }
    
     
    
}
