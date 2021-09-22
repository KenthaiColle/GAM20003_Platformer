using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] backgrounds;     // Array (list) of all the back- and foregrounds to be parallaxed
    private float[] parallaxScales;     // The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;        // How smooth the parallax is going to be. Make sure to set this above 0
    private Transform cam;              // reference to the main cameras transform
    private Vector3 previousCamPos;     // the position of the camera in the previous frame
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
