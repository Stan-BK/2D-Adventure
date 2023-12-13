using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D cc2d;
    public CinemachineImpulseSource impulseSource;
    public CameraEventSo cameraEventSo;
    // Start is called before the first frame update
    void Start()
    {
        cc2d = GetComponent<CinemachineConfiner2D>();
        GetBoundsForCamera();
    }

    public void GetBoundsForCamera()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null) return;

        cc2d.m_BoundingShape2D = obj.GetComponent<PolygonCollider2D>();
        
        cc2d.InvalidateCache();
    }

    private void OnEnable()
    {
        cameraEventSo.OnCameraShake += OnCameraShake;
    }

    private void OnDisable()
    {
        cameraEventSo.OnCameraShake -= OnCameraShake;
    }

    private void OnCameraShake()
    {
        impulseSource.GenerateImpulse();
    }
}
