using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRenderer : PhotoRenderer
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
        Graphics.Blit(src,dest,DepthMaterial);
    }
}
