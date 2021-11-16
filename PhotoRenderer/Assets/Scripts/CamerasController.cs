using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    public List<PhotoRenderer> cameras;

    public RenderTexture ModelTexture;
    public RenderTexture EnvironmentTexture;

    public Texture2D alfa;
    public Texture2D beta;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LimitationsCheck();
            
            foreach (var camera in cameras)
            {
                camera.ChangeBool();
            }
        }
    }

    private void LimitationsCheck()
    {
        Texture2D texture1 = new Texture2D (ModelTexture.width, ModelTexture.height, TextureFormat.ARGB32, false);
        RenderTexture.active = ModelTexture;
        texture1.ReadPixels (new Rect (0, 0, ModelTexture.width, ModelTexture.height), 0, 0);
        texture1.Apply ();
        alfa = texture1;
        
        Texture2D texture2 = new Texture2D (ModelTexture.width, ModelTexture.height, TextureFormat.ARGB32, false);
        RenderTexture.active = EnvironmentTexture;
        texture2.ReadPixels (new Rect (0, 0, ModelTexture.width, ModelTexture.height), 0, 0);
        texture2.Apply ();
        beta = texture2;

        if (ModelExistCheck(texture1,texture2))
        {
            Debug.Log("Model is not in a view of camera");
        }

        if (CrossingCheck(texture1,texture2))
        {
            Debug.Log("Model is crossing");
        }
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
