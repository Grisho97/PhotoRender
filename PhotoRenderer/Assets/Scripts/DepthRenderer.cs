using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DepthRenderer : PhotoRenderer
{
    public Material DepthMaterial;
    public Shader DepthShader;
    
    
    public override void OnEnable()
    {
        base.OnEnable();
        DepthMaterial = new Material(DepthShader);
    }
    

    public override void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture.active = EnvironmentTexture;
        Graphics.Blit(EnvironmentTexture,dest,DepthMaterial);
        base.OnRenderImage(src,dest);
        
    }
}
