using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRenderer : PhotoRenderer 
{
    public override void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src,dest);
        base.OnRenderImage(src,dest);
    }
}