using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


public class PhotoRenderer : MonoBehaviour
{
    public string Folder;
    public string PictureName;
    
    public Camera camera;
    public int index = 0;
    
    public int width; 
    public int height;

    public bool GetScreenShot = false;
    
    public virtual void OnEnable()
    {
        camera = GetComponent<Camera>();
        camera.depthTextureMode |= DepthTextureMode.Depth;
    }

    private void OnDisable()
    {
        
    }

    public virtual void Start()
    {
        width = this.camera.pixelWidth;
        height = this.camera.pixelHeight;
    }

    public virtual void ChangeBool()
    {
        GetScreenShot = true;
    }

    public virtual void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (GetScreenShot)
        {
           GetTexture();
           GetScreenShot = false;
        }
    }

    public void GetTexture()
    {
        Texture2D texture = new Texture2D (width, height, TextureFormat.ARGB32, false);
        texture.ReadPixels (new Rect (0, 0, width, height), 0, 0);
        texture.Apply ();
        
        byte[] bytes = texture.EncodeToPNG();

        index++;
        Directory.CreateDirectory(Application.dataPath + "/OutPut/" + Folder +"/");
        File.WriteAllBytes(Application.dataPath + "/OutPut/" + Folder +"/" + PictureName + index + ".png", bytes);
    }
}
