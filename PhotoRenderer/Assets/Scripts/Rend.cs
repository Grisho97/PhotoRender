using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rend : MonoBehaviour {
 
   public Material mat;
   public Shader Shader;
   
   public Material mat2;
   public Shader Shader2;

   public Camera camera;
   public Camera camera2;

   private RenderTexture main;

   private void OnEnable()
   {
       mat = new Material(Shader);
       mat2 = new Material(Shader2);
       camera2.cullingMask = 1 << 8;
       camera.cullingMask = -1;
       //camera2 = camera;
       main = RenderTexture.GetTemporary (camera.pixelWidth, camera.pixelHeight, 16, RenderTextureFormat.ARGB32);
   }

   void Start()
   {
       camera.depthTextureMode = DepthTextureMode.Depth;
       camera2.depthTextureMode = DepthTextureMode.Depth;
    }
 
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //main = RenderTexture.active;
        BackMask(source);

        NewMask(source,destination);
       
    }

    private void BackMask(RenderTexture source)
    {
        RenderTexture Mask = RenderTexture.GetTemporary (camera.pixelWidth, camera.pixelHeight, 16, RenderTextureFormat.ARGB32);
        Graphics.Blit(source, Mask,mat);
        mat2.SetTexture("_Tex", Mask);
    }

    private void NewMask(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat2);
    }

    private void OnPreRender()
    {
        
    }

    private void OnPostRender()
    {
       // camera.cullingMask = -1;
    }
    
    
}
