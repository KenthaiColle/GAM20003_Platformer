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
    [SerializeField] bool vertical;

    private float originalXPos;
    private float originalYPos;

    [SerializeField] float finalDistance;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        wd = Door.GetComponent<WindmillDoor>();
        originalXPos = wd.transform.position.x;
        originalYPos = wd.transform.position.y;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vertical == true)
        {
            if (originalYPos > wd.transform.position.y + finalDistance) //if the position is greater than the door's original pos + the distance the door should move then it stops
            {
                wd.rb.velocity = new Vector2(0, 0);
            }
        }
        else
        {
            if (originalXPos > wd.transform.position.x + finalDistance) //if the position is greater than the door's original pos + the distance the door should move then it stops
            {
                wd.rb.velocity = new Vector2(0, 0);
            }
            else if (originalXPos < wd.transform.position.x - finalDistance) //if the position is less than the door's original pos - the distance the door should move then it stops
            {
                wd.rb.velocity = new Vector2(0, 0);
            }
        }
        
        if(active == true)
        {
            anim.SetBool("Activated", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(vertical == true)
        {
            if (other.gameObject.tag == "Wind")
            {
                wd.rb.velocity = new Vector2(0, doorSpeed);
                active = true;
                wd.coll.enabled = false;
            }
        }
        else
        {
            if (doorMoveLeft == true) //If door move left is true
            {
                if (other.gameObject.tag == "Wind")
                {
                    wd.rb.velocity = new Vector2(-doorSpeed, 0);
                    active = true;
                    wd.coll.enabled = false;
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



}
