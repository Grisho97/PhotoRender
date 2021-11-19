using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRenderer : PhotoRenderer
{
    public override void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src,dest);
        
        if (GetScreenShot)
        {
            RenderTexture Mask = RenderTexture.GetTemporary (width, height, 16, RenderTextureFormat.ARGB32); 
            Graphics.Blit(src,Mask);
            DefaultTexture = Mask;
            
            GetScreenShot = false;
        }
        
        base.OnRenderImage(src,dest);
    }
    
}
