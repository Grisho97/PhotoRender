using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class RenderLogic : MonoBehaviour
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
    private List<CameraTransformData> cameraTransforms;
    
    private ICameraStrategy cameraStrategy;
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

            switch (cameraParameters.CameraSrategyName)
            {
                case "Rotate360":
                    cameraStrategy = new Rotate360();
                    cameraParameters = new CameraParametersRotate360();
                    cameraParameters = JsonUtility.FromJson<CameraParametersRotate360>(
                        File.ReadAllText(Application.dataPath + "/Resources/CameraParameters/" + config.CameraStrategyFileName + ".json"));
                    break;
                case "WallMove":
                    cameraStrategy = new WallMove();
                    cameraParameters = new CameraParametersWallMove();
                    cameraParameters = JsonUtility.FromJson<CameraParametersWallMove>(
                        File.ReadAllText(Application.dataPath + "/Resources/CameraParameters/" + config.CameraStrategyFileName + ".json"));
                    break;
                case "SecurityCamera":
                    cameraStrategy = new SecurityCamera();
                    cameraParameters = new SecurityCamera.CameraParametersSecurityCamera();
                    cameraParameters = JsonUtility.FromJson<SecurityCamera.CameraParametersSecurityCamera>(
                        File.ReadAllText(Application.dataPath + "/Resources/CameraParameters/" + config.CameraStrategyFileName + ".json"));
                    break;
            }
            
            cameraTransforms = cameraStrategy.GetCameraPositions(cameraParameters, cameraTransform);

            StartCoroutine(NextStep());
        }
    }

    IEnumerator NextStep()
    {
        yield return null;
        for (int i = 0; i < transformData.ModelTransformsList.Count; i++)
        {
            for (int j = 0; j < cameraTransforms.Count; j++)
            {
                SetParameters(i,j);
                camerasController.MakeRenderers();
                Debug.Log("Render");
                yield return null;
            }
        }
    }

    private void SetParameters(int i, int j)
    {
        cameraTransform.position = cameraTransforms[j].cameraPosition;
        cameraTransform.eulerAngles = cameraTransforms[j].cameraRotation;
        modelTransform.position = transformData.ModelTransformsList[i].modelPosition;
        modelTransform.eulerAngles = transformData.ModelTransformsList[i].modelRotation;
    }
}

[Serializable]
public struct ConfigurationFile
{
    public string EnvironmentFileName;
    public string ModelFileName;
    public string LightFileName;
    public string PositionsFileName;
    public string CameraStrategyFileName;
}

[Serializable]
public class CameraTransformData: CameraParameters
{
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;
}
