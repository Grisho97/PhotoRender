using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBuilder : MonoBehaviour
{
    [Header("Environment parameters")]
    [SerializeField] private GameObject environmentPrefab;

    [Space]
    [SerializeField] private float xPositionEnv;
    [SerializeField] private float yPositionEnv;
    [SerializeField] private float zPositionEnv;
    [Space] 
    [SerializeField] private float xRotationEnv;
    [SerializeField] private float yRotationEnv;
    [SerializeField] private float zRotationEnv;
    
    [Space]
    [Header("Model parameters")]
    public GameObject modelPrefab;
    
    [Space]
    [SerializeField] private float xPositionModel;
    [SerializeField] private float yPositionModel;
    [SerializeField] private float zPositionModel;
    [Space] 
    [SerializeField] private float xRotationModel;
    [SerializeField] private float yRotationModel;
    [SerializeField] private float zRotationModel;

    private void Awake()
    {
        PrefabInstance(environmentPrefab,xPositionEnv,yPositionEnv,zPositionEnv,xRotationEnv,yRotationEnv,zRotationEnv);
        PrefabInstance(modelPrefab,xPositionModel,yPositionModel,zPositionModel,xRotationModel,yRotationModel,zRotationModel);
    }

    private void PrefabInstance(GameObject prefab, float xPos, float yPos, float zPos, float xRot, float yRot, float zRot)
    {
        Instantiate(prefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(xRot, yRot, zRot));
    }

    
}
