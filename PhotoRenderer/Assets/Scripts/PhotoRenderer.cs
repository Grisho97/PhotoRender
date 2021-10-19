using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PhotoRenderer : MonoBehaviour
{

    public string screenShotName = "Test";
    public Camera camera;

    private int index = 0;
    private static readonly int CameraDepthTexture = Shader.PropertyToID("_CameraDepthTexture");

    private void OnEnable()
    {
        camera.depthTextureMode |= DepthTextureMode.Depth;
    }

    void Start()
    {
        //StartCoroutine(MakeScreenShot());
        StartCoroutine(MakeDepthMap());
    }

    IEnumerator MakeDepthMap()
    {
        yield return new WaitForEndOfFrame();
        int width = this.camera.pixelWidth;
        int height = this.camera.pixelHeight;

        //The depth texture is set as a shader global under the variable name '_CameraDepthTexture' - we can just get a reference to that global
        RenderTexture depth = Shader.GetGlobalTexture (CameraDepthTexture) as RenderTexture;
        //The depth is technically stored in the depth buffer which we can't access directly, however if we blit that texture into another texture it will copy the depth into the red colour channel
        RenderTexture tmp = RenderTexture.GetTemporary (width, height, 16, RenderTextureFormat.ARGB32);
        Graphics.Blit (depth, tmp);

        yield return null;
        
        //Store the last active render texture and set our depth copy as active
        RenderTexture lastActive = RenderTexture.active;
        RenderTexture.active = tmp;
        
        
        Texture2D texture = new Texture2D (width, height, TextureFormat.ARGB32, false);
        texture.ReadPixels (new Rect (0, 0, width, height), 0, 0);
        texture.Apply ();
        
        byte[] bytes = texture.EncodeToPNG();

        index++;
        File.WriteAllBytes(Application.dataPath + "/ScreenShots/" + screenShotName + index + ".png", bytes);
        
        //Restore the active render texture and release our temporary tex
        RenderTexture.active = lastActive;
        RenderTexture.ReleaseTemporary (tmp);
    }

    IEnumerator MakeScreenShot()
    {
        yield return new WaitForEndOfFrame();
        int width = this.camera.pixelWidth;
        int height = this.camera.pixelHeight;
        
        Texture2D texture = new Texture2D(width, height);

        Rect rect = new Rect(0,0,width,height);
        
        
        texture.ReadPixels(rect,0,0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();

        index++;
        File.WriteAllBytes(Application.dataPath + "/ScreenShots/" + screenShotName + index + ".png", bytes);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(MakeScreenShot());
            StartCoroutine(MakeDepthMap());
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
