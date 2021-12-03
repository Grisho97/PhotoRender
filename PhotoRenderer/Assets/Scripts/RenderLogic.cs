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
    private List<TransformData> transformData;

    [SerializeField] 
    private List<CameraParameters> cameraParameters;
    
    private List<CameraTransformData> cameraTransforms;

    [SerializeField]
    private ConfigurationFile config;
    private List<ConfigurationData> configurationData;
    private ICameraStrategy cameraStrategy;
    private CamerasController camerasController;
    private Transform cameraTransform;
    private Transform modelTransform;

    private int index;

    private void Awake()
    {
        camerasController = Cameras.GetComponent<CamerasController>();
        cameraTransform = Cameras.GetComponent<Transform>();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.U))
        // {
        //     File.WriteAllText(Application.dataPath + "/Resources/ConfigurationFile/ConfigurationFile1.json", JsonUtility.ToJson(config));
        // }

        if (Input.GetKeyDown(KeyCode.B))
        {
            BuildAllPrefabs();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildAllPrefabs();
            StartCoroutine(NextStep());
        }
    }

    private void BuildAllPrefabs()
    {
        config = JsonUtility.FromJson<ConfigurationFile>(
            File.ReadAllText(Application.dataPath + "/Resources/ConfigurationFile/ConfigurationFile.json"));

        configurationData = config.ConfigurationDatas;
        
        EnvironmentPrefabs = new List<GameObject>();
        ModelPrefabs = new List<GameObject>();
        LightPrefabs = new List<GameObject>();

        for (int i = 0; i < configurationData.Count; i++)
        {
            EnvironmentPrefabs.Add(Resources.Load("EnvironmentPrefabs/" +
                                                              configurationData[i].EnvironmentFileName) as GameObject);
            Instantiate(EnvironmentPrefabs[i]);
            EnvironmentPrefabs[i].SetActive(false);

            ModelPrefabs.Add(Resources.Load<GameObject>("ModelPrefabs/" +
                                                        configurationData[i].ModelFileName));
            Instantiate(ModelPrefabs[i]);
            ModelPrefabs[i].SetActive(false);

            LightPrefabs.Add(Resources.Load<GameObject>("LightPrefabs/" +
                                                        configurationData[i].LightFileName));
            Instantiate(LightPrefabs[i]);
            LightPrefabs[i].SetActive(false);
            
            
            transformData.Add(JsonUtility.FromJson<TransformData>(
                File.ReadAllText(Application.dataPath + "/Resources/ModelPositions/" +
                                 configurationData[i].PositionsFileName + ".json")));
            
            cameraParameters.Add(JsonUtility.FromJson<CameraParameters>(
                File.ReadAllText(Application.dataPath + "/Resources/CameraParameters/" +
                                 configurationData[i].CameraStrategyFileName + ".json")));

            switch (cameraParameters[i].CameraSrategyName)
            {
                case "Rotate360":
                    cameraStrategy = new Rotate360();
                    cameraParameters[i] = new CameraParametersRotate360();
                    cameraParameters[i] = JsonUtility.FromJson<CameraParametersRotate360>(
                        File.ReadAllText(Application.dataPath + "/Resources/CameraParameters/" +
                                         configurationData[i].CameraStrategyFileName + ".json"));
                    break;
                case "WallMove":
                    cameraStrategy = new WallMove();
                    cameraParameters[i] = new CameraParametersWallMove();
                    cameraParameters[i] = JsonUtility.FromJson<CameraParametersWallMove>(
                        File.ReadAllText(Application.dataPath + "/Resources/CameraParameters/" +
                                         configurationData[i].CameraStrategyFileName + ".json"));
                    break;
                case "SecurityCamera":
                    cameraStrategy = new SecurityCamera();
                    cameraParameters[i] = new SecurityCamera.CameraParametersSecurityCamera();
                    cameraParameters[i] = JsonUtility.FromJson<SecurityCamera.CameraParametersSecurityCamera>(
                        File.ReadAllText(Application.dataPath + "/Resources/CameraParameters/" +
                                         configurationData[i].CameraStrategyFileName + ".json"));
                    break;
            }
        }
    }

    IEnumerator NextStep()
    {
        yield return null;
        for (int i = 0; i < configurationData.Count; i++)
        {
            EnvironmentPrefabs[i].SetActive(true);
            ModelPrefabs[i].SetActive(true);
            LightPrefabs[i].SetActive(true);
            
            modelTransform = ModelPrefabs[i].GetComponent<Transform>();
            cameraTransforms = cameraStrategy.GetCameraPositions(cameraParameters[i], cameraTransform);

            for (int k = 0; k < transformData[i].ModelTransformsList.Count; k++)
            {
                for (int j = 0; j < cameraTransforms.Count; j++)
                {
                    index++;
                    Directory.CreateDirectory(Application.dataPath + "/OutPut/Render_" + index +"/");
                    SetParameters(i, k, j);
                    camerasController.MakeRenderers(index);
                    SaveLogFile(i);
                    Debug.Log("Render");
                    yield return null;
                }
            }
            
            EnvironmentPrefabs[i].SetActive(false);
            ModelPrefabs[i].SetActive(false);
            LightPrefabs[i].SetActive(false);
        }
    }

    private void SetParameters(int i, int k, int j)
    {
        cameraTransform.position = cameraTransforms[j].cameraPosition;
        cameraTransform.eulerAngles = cameraTransforms[j].cameraRotation;
        modelTransform.position = transformData[i].ModelTransformsList[k].modelPosition;
        modelTransform.eulerAngles = transformData[i].ModelTransformsList[k].modelRotation;
    }

    private void SaveLogFile(int i)
    {
        RenderParameters renderParameters = new RenderParameters()
        {
            configurationParameters = configurationData[i],
            modelPosition = modelTransform.position,
            modelRotation = modelTransform.eulerAngles,
            cameraPosition = cameraTransform.position,
            cameraRotation = cameraTransform.eulerAngles,
            ModelIsCrossing = camerasController.IsCrossing(),
            ModelOutOfView = camerasController.OutOfView()
        };
        
        File.WriteAllText(Application.dataPath + "/OutPut/Render_" + index +"/" + "LogFile.json", JsonUtility.ToJson(renderParameters));
    }
}

[Serializable]
public struct ConfigurationFile
{
    public List<ConfigurationData> ConfigurationDatas;
}

[Serializable]
public struct ConfigurationData
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

[Serializable]
public struct RenderParameters
{
    public ConfigurationData configurationParameters;
    
    public Vector3 modelPosition;
    public Vector3 modelRotation;
    
    public Vector3 cameraPosition;
    public Vector3 cameraRotation;

    public bool ModelIsCrossing;
    public bool ModelOutOfView;
}
