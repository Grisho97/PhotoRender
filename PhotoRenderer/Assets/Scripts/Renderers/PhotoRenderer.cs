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
    public int index;
    
    public int width; 
    public int height;

    public bool GetScreenShot = false;

    public RenderTexture DefaultTexture;
    public RenderTexture ModelTexture;
    public RenderTexture EnvironmentTexture;
    
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

    public virtual void ChangeBool(int indexCC)
    {
        index = indexCC;
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
        
        File.WriteAllBytes(Application.dataPath + "/OutPut/Dataset/Render_" + index +"/" + PictureName + ".png", bytes);
    }
}
