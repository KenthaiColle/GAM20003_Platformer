﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Camera camera;
    float cameraZoom = 20f;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        camera.orthographicSize = cameraZoom;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); //only take the x and y from the player, Z stays the same.
    }
}