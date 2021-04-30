﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerTargetcam : MonoBehaviour
{
    public List<Transform> targets;
    public float smoothTime = .5f;
    public Vector3 offset;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    private Vector3 velocity;


    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;
        Move();
        Zoom();
    }

    void Zoom()
    {
        
    }

    void Move()
    {

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }


        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
