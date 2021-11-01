using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rend : MonoBehaviour {
 
   public Material mat;
   public Shader Shader;

   private void OnEnable()
   {
       mat = new Material(Shader);
   }

   void Start()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }
 
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination,mat);
    }
}
