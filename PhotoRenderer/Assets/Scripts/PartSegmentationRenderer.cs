using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSegmentationRenderer : PhotoRenderer
{
    public Material PartSigmentationMaterial;
    public Shader PartSigmentationShader;

    //public FullSegmentationRenderer s;

    public override void OnEnable()
    {
        base.OnEnable();
        PartSigmentationMaterial = new Material(PartSigmentationShader);
    }
    

    public override void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        PartSigmentationMaterial.SetTexture("_Tex", ModelTexture);
        
        Graphics.Blit(EnvironmentTexture,dest,PartSigmentationMaterial);
        base.OnRenderImage(src,dest);
    }
}
