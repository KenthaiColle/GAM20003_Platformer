using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField]
    Transform rotationCenter; //Transform where the point of rotation is

    [SerializeField]
    float rotationRadius = 2f, angularSpeed = 2f;

    float posX, posY, angle = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Trigonomitry 
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360f) // reset the angle when it reaches full circle.
        {
            angle = 0f;
        }
            
    }
}
