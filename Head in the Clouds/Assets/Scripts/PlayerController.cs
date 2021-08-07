using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Start() Variable
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll; //Boxcollider 2D and other collider2D can fall into Collider2D since Boxcollider inherit Collider2D


    //Finite State Machine
    private enum StateList {idle, running, jumping, falling}
    private StateList playerState = StateList.idle;


    //Inspector variable
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float playerJumpForce = 12f;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        AnimationState();
        anim.SetInteger("state", (int)playerState); //int in front of state convert the enum into integer. Sets animation based on enum state
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //moving left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true; //flips the character
        }

        //moving right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }


        //GetKeyDown just take the input once (even if hold) while GetKey is constant.
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, playerJumpForce);
            playerState = StateList.jumping;
        }
    }

    //Methods that checks the state of the player for animations
    private void AnimationState()
    {

        if(playerState == StateList.jumping) //check if state is jumping, if it is it won't run the elseif
        {
            if(rb.velocity.y < .1f) //check if the player's y velocity is less than 1f
            {
                playerState = StateList.falling; //set player's state to falling
            }
        }

        if(playerState == StateList.falling) //check if state is falling
        {
            if (coll.IsTouchingLayers(ground))//check if the player is touching the ground layer or not.
            {
                playerState = StateList.idle; //set state to idle
            }
        }

        else if ((rb.velocity.y) < -4f) //
        {
            playerState = StateList.falling;
        }
        else if(Mathf.Abs(rb.velocity.x) > 2f) //Mathf.Abs always return the positive value, checks if velocity is bigger than 2f which will result in a little slide at the end
        {
            //Moving
            playerState = StateList.running; //set state to running
        }

        else
        {
            playerState = StateList.idle; //set state to idle
        }
    }


}
