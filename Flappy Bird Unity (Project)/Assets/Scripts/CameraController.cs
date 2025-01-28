using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static public CameraController instance;

    public Camera cam;

    void Awake()
    {
            instance = this; 
    }

    public void Shake(float strenght, float duration)
    {
        cam.DOShakePosition(duration, strenght);
    }
}
