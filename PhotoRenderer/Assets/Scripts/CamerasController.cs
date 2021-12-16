using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    public List<PhotoRenderer> cameras;

    public RenderTexture ModelTexture;
    public RenderTexture EnvironmentTexture;

    private Texture2D alfa;
    private Texture2D beta;

    private bool modelIsCrossing;
    private bool modelIsOutOfView;
    private int indexCC;

    public void MakeRenderers(int index)
    {
        indexCC = index;
        
        LimitationsCheck();
            
        foreach (var camera in cameras)
        {
            camera.ChangeBool(indexCC);
        }
    }

    private void LimitationsCheck()
    {
        Texture2D texture1 = new Texture2D (ModelTexture.width, ModelTexture.height, TextureFormat.ARGB32, false);
        RenderTexture.active = ModelTexture;
        texture1.ReadPixels (new Rect (0, 0, ModelTexture.width, ModelTexture.height), 0, 0);
        texture1.Apply();
        alfa = texture1;
        
        Texture2D texture2 = new Texture2D (ModelTexture.width, ModelTexture.height, TextureFormat.ARGB32, false);
        RenderTexture.active = EnvironmentTexture;
        texture2.ReadPixels (new Rect (0, 0, ModelTexture.width, ModelTexture.height), 0, 0);
        texture2.Apply();
        beta = texture2;

        if (ModelExistCheck(texture1,texture2))
        {
            modelIsOutOfView = true;
            //Debug.Log("Model is not in a view of camera");
        }
        else
        {
            modelIsOutOfView = false;
        }

        if (CrossingCheck(texture1,texture2))
        {
            modelIsCrossing = true;
            // Debug.Log("Model is crossing");
        }
        else
        {
            modelIsCrossing = false;
        }
    }

    public bool IsCrossing()
    {
        return modelIsCrossing;
    }

    public bool OutOfView()
    {
        return modelIsOutOfView;
    }
    
    private bool ModelExistCheck (Texture2D first, Texture2D second)
    {
        Color[] firstPix = first.GetPixels();
        Color[] secondPix = second.GetPixels();
        if (firstPix.Length!= secondPix.Length)
        {
            Debug.Log("Incorrect formats");
            return false;
        }
        for (int i= 0;i < firstPix.Length;i++)
        {
            if (firstPix[i] != Color.black)
            {
                return false;
            }
        }
        return true;
    }
    
    private bool CrossingCheck (Texture2D first, Texture2D second)
    {
        Color[] firstPix = first.GetPixels();
        Color[] secondPix = second.GetPixels();
        if (firstPix.Length!= secondPix.Length)
        {
            return false;
        }
        for (int i= 0;i < firstPix.Length;i++)
        {
            if (firstPix[i] == secondPix[i] && firstPix[i] != Color.black)
            {
                return true;
            }
        }

        return false;
    }
}
