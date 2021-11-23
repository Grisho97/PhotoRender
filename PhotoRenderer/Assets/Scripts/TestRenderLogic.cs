using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class TestRenderLogic : MonoBehaviour
{
    public GameObject Cameras;
    public List<GameObject> EnvironmentPrefabs;
    public List<GameObject> ModelPrefabs;
    public List<GameObject> LightPrefabs;

    [SerializeField]
    private TransformData transformData;

    [SerializeField] 
    private CameraParameters cameraParameters;

    [SerializeField]
    private List<Transform> cameraTransforms;
    
    private CameraStrategy cameraStrategy;
    private CamerasController camerasController;
    private Transform cameraTransform;
    private Transform modelTransform;


    private void Awake()
    {
        camerasController = Cameras.GetComponent<CamerasController>();
        cameraTransform = Cameras.GetComponent<Transform>();
        modelTransform = ModelPrefabs[0].GetComponent<Transform>();
    }


    private void Start()
    {
        BuildAllPrefabs();
    }

    private void BuildAllPrefabs()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ConfigurationFile config = JsonUtility.FromJson<ConfigurationFile>(
                File.ReadAllText(Application.dataPath + "/Resources/ConfigurationFile/ConfigurationFile.json"));
            
            transformData =
                JsonUtility.FromJson<TransformData>(
                    File.ReadAllText(Application.dataPath + "/Resources/ModelPositions/" + config.PositionsFileName +".json"));
            
            cameraParameters = JsonUtility.FromJson<CameraParameters>(
                File.ReadAllText(Application.dataPath + "/Resources/CameraParameters/" + config.CameraStrategyFileName + ".json"));
            
            cameraStrategy = new CameraStrategy();
            cameraTransforms = cameraStrategy.GetCameraPositions(cameraParameters);
            
            StartCoroutine(NextStep());
        }
    }

    IEnumerator NextStep()
    {
        for (int i = 0; i < transformData.ModelTransformsList.Count; i++)
        {
            for (int j = 0; j < cameraTransforms.Count; j++)
            {
                SetParameters(i,j);
                camerasController.MakeRenderers();
                Debug.Log("Render");
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void SetParameters(int i, int j)
    {
        cameraTransform = cameraTransforms[j];
        modelTransform.position = transformData.ModelTransformsList[i].modelPosition;
        modelTransform.eulerAngles = transformData.ModelTransformsList[i].modelRotation;
    }
}

public struct ConfigurationFile
{
    public string EnvironmentFileName;
    public string ModelFileName;
    public string LightFileName;
    public string PositionsFileName;
    public string CameraStrategyFileName;
}
