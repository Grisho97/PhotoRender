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
    void Start()
    {
        StartCoroutine(MakeScreenShot());
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
            StartCoroutine(MakeScreenShot());
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
