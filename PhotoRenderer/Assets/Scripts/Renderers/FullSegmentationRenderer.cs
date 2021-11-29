using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSegmentationRenderer : PhotoRenderer
{
    public Material FullSigmentationMaterial;
    public Shader FullSigmentationShader;
    
    public Texture maskTex;
    
    public override void OnEnable()
    {
        base.OnEnable();
        FullSigmentationMaterial = new Material(FullSigmentationShader);
        maskTex = null;
    }


    public override void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(ModelTexture,dest,FullSigmentationMaterial);
        
        if (maskTex == null)
        {
            width = this.camera.pixelWidth;
            height = this.camera.pixelHeight;
            RenderTexture Mask = RenderTexture.GetTemporary (width, height, 16, RenderTextureFormat.ARGB32);
            Graphics.Blit(src,Mask,FullSigmentationMaterial);
            maskTex = Mask;
        }
        
        base.OnRenderImage(src,dest);
    }
}
