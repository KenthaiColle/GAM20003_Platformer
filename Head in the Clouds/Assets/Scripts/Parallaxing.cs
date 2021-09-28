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


    //called before start(). Great for references
    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        // The previous frame had the current frame's camera position
        previousCamPos = cam.position;

        // asigning coresponding parallaxScales
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z; //Set each background to the coresponding parallx scale
        }
    }

    // Update is called once per frame
    void Update()
    {
        // for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallaxXPos = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallaxYPos = (previousCamPos.y - cam.position.y) * parallaxScales[i];

            // set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallaxXPos;

            // set a target Y position which is the current position plus the parallax
            float backgroundTargetPosY = backgrounds[i].position.y + parallaxYPos;

            // create a target position which is the background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);

            // fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        // set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
