using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICameraStrategy
{
    public List<CameraTransformData> GetCameraPositions(CameraParameters cameraParameters, Transform cameraTransform);

    public CameraParameters GetCameraParameters(CameraParameters cameraParameters);
}
