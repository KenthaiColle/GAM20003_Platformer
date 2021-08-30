﻿using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Start() Variable
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll; //Boxcollider 2D and other collider2D can fall into Collider2D since Boxcollider inherit Collider2D

    //Finite State Machine
    private enum StateList {idle, running, jumping, falling, hurt}
    private StateList playerState = StateList.idle;


    //Inspector variable
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float playerJumpForce = 12f;
    [SerializeField] private int cherriesAmount = 0;
    [SerializeField] private Text collectableText;
    [SerializeField] private float hurtForce = 10f;


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
        if(playerState != StateList.hurt) //If player is hurt, movement cannot occur.
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)playerState); //int in front of state convert the enum into integer. Sets animation based on enum state

        //Debug.Log(rb.velocity.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject); //destroy the collectable
            cherriesAmount += 1;
            collectableText.text = "Cherries: " + cherriesAmount.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Ouch");
            if(playerState == StateList.falling)
            {
                Destroy(other.gameObject);
                jump();
            }
            else
            {
                playerState = StateList.hurt;
                Debug.Log(playerState);
                if (other.gameObject.transform.position.x > transform.position.x)//If the enemy's position is greater than the player's then it's to the player's right
                {
                    //Therefore move the player left and take damage
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                    Debug.Log("worK");
                    Debug.Log(rb.velocity.x);
                }
                else
                {
                    //enemy is to the left so move the player right.
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        var hDirectionTap = Input.GetButtonDown("Horizontal");

        //moving left
        if (hDirection < 0)
        {
            if (rb.velocity.x >= -speed && coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = true; //flips the character
            }
            //else
            //{
            //    rb.velocity = new Vector2(-rb.velocity.x + .5f, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
            //    gameObject.GetComponent<SpriteRenderer>().flipX = true;
            //}
            
        }

        //moving right
        else if (hDirection > 0)
        {
            if(rb.velocity.x <= speed && coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                Debug.Log("should not work");
            }
            //else
            //{
            //    rb.velocity = new Vector2(rb.velocity.x - .5f, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
            //    gameObject.GetComponent<SpriteRenderer>().flipX = false;
            //}
        }

        if (Input.GetButtonDown("Horizontal") && playerState == StateList.jumping || playerState == StateList.falling)
        {
            if (hDirection < 0) 
            {
                if (rb.velocity.x > 0)// check if val is already negative
                {
                    rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                //else
                //{
                //    rb.velocity = new Vector2(rb.velocity.x - .5f, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
                //    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                //}
            }
            if (hDirection > 0)
            {
                if(rb.velocity.x < 0)// check if val is already negative
                {
                    rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
                    gameObject.GetComponent<SpriteRenderer>().flipX = false; //Make this flip right and flip left funtion.
                }
                //else
                //{
                //    rb.velocity = new Vector2(rb.velocity.x + .5f, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
                //    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                //}
                
            }
        }

        //GetKeyDown just take the input once (even if hold) while GetKey is constant.
        if (Input.GetButtonDown("Jump")) // && coll.IsTouchingLayers(ground)
        {
            if (rb.velocity.x > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x + 5f, playerJumpForce); //Everytime we jump/flap wings we go faster REMEMBER TO CHANGE THAT FREAKING MAGIC NUMBER
                playerState = StateList.jumping;
            }
            else if (rb.velocity.x < 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x - 5f, playerJumpForce);
                playerState = StateList.jumping;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, playerJumpForce);
                playerState = StateList.jumping;
            }

        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            transform.position = new Vector2(transform.position.x + rb.velocity.x, transform.position.y);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (rb.velocity.x > 0f)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (rb.velocity.x < 0f)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

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

        else if(playerState == StateList.falling) //check if state is falling
        {
            if (coll.IsTouchingLayers(ground))//check if the player is touching the ground layer or not.
            {
                playerState = StateList.idle; //set state to idle
            }
        }
        else if (playerState == StateList.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f) //If player is idle/stopped, the staate chaanges back to idle
            {
                playerState = StateList.idle;
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
    
    private void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, playerJumpForce); //Everytime we jump/flap wings we go faster REMEMBER TO CHANGE THAT FREAKING MAGIC NUMBER
        playerState = StateList.jumping;
    }
}