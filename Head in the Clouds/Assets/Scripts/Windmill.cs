using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Windmill : MonoBehaviour
{

    public GameObject Door;
    public WindmillDoor wd;

    [SerializeField] float doorSpeed = 10f;

    private bool active = false;
    [SerializeField] bool doorMoveLeft;

    private float originalXPos;

    [SerializeField] float finalDistance; 

    // Start is called before the first frame update
    void Start()
    {
        wd = Door.GetComponent<WindmillDoor>();
        originalXPos = wd.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(originalXPos > wd.transform.position.x + finalDistance) //if the position is greater than the door's original pos + the distance the door should move then it stops
        {
            wd.rb.velocity = new Vector2(0, 0);
        }
        else if (originalXPos < wd.transform.position.x - finalDistance) //if the position is less than the door's original pos - the distance the door should move then it stops
        {
            wd.rb.velocity = new Vector2(0, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (doorMoveLeft) //If door move left is true
        {
            if (other.gameObject.tag == "Wind")
            {
                wd.rb.velocity = new Vector2(-doorSpeed, 0);
                active = true;
            }
        }
        else //If the door should move right
        {
            if (other.gameObject.tag == "Wind")
            {
                wd.rb.velocity = new Vector2(doorSpeed, 0);
                active = true;
            }
        }
    }



}
