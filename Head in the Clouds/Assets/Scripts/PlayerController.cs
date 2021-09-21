using System.Collections;
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
    private enum StateList {idle, running, jumping, falling, hurt, blinking, braking}
    private StateList playerState = StateList.idle;
    private bool finishedBlink = false;
    public bool gameFinished = false;

    //Inspector variable
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float playerJumpForce = 12f;
    [SerializeField] private int keysAmount = 0;
    [SerializeField] private Text collectableText;
    [SerializeField] private float hurtForce = 10f;

    public GameObject wind;
    public Wind windScript;

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
        
        if (playerState != StateList.hurt) //If player is hurt, movement cannot occur.
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
            BoxCollider2D collider = collision.gameObject.GetComponent<BoxCollider2D>(); //destroy the collectable
            SpriteRenderer sprite = collision.gameObject.GetComponent<SpriteRenderer>();

            collider.enabled = false;
            sprite.enabled = false;


            keysAmount += 1;
            collectableText.text = "Keys: " + keysAmount.ToString();
        }
        if (collision.gameObject.tag == "HighScore")
        {
            gameFinished = true;
            Debug.Log(gameFinished);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
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
                gameObject.GetComponent<SpriteRenderer>().flipX = false; //flips the character
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
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            //else
            //{
            //    rb.velocity = new Vector2(rb.velocity.x - .5f, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
            //    gameObject.GetComponent<SpriteRenderer>().flipX = false;
            //}
        }

        if (Input.GetButtonDown("Horizontal") && playerState == StateList.jumping || playerState == StateList.falling || playerState == StateList.blinking || playerState == StateList.braking)
        {
            if (hDirection < 0)
            {
                if (rb.velocity.x > 0)// check if val is already negative
                {
                    rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }

            }
            if (hDirection > 0)
            {
                if (rb.velocity.x < 0)// check if val is already negative
                {
                    rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
                    gameObject.GetComponent<SpriteRenderer>().flipX = true; //Make this flip right and flip left funtion.
                }

            }

            //If the player just jump without moving so vel for x == 0. Then move.
            if (rb.velocity.x == 0)
            {
                if (hDirection < 0)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }

                if (hDirection > 0)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y); //rb.velocity.y allows the gravity to work instead of hard coding 0 which just makes the player stay at 0
                    gameObject.GetComponent<SpriteRenderer>().flipX = true; //Make this flip right and flip left funtion.
                }
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
        
        if (Input.GetButtonDown("Fire1")) //Blink teleport
        {
            transform.position = new Vector2(transform.position.x + rb.velocity.x, transform.position.y);
            playerState = StateList.blinking;
             anim.SetInteger("state", (int)playerState);

        }
        if (Input.GetButtonDown("Fire2")) //air brake
        {
            if(rb.velocity.x > 10f)
            {
                GameObject newWind = (GameObject)Instantiate(wind, transform.position, transform.rotation) as GameObject;
                windScript = newWind.GetComponent<Wind>();
                windScript.BlowRight();
            }
            if (rb.velocity.x < -10f)
            {
                GameObject newWind = Instantiate(wind, transform.position, transform.rotation) as GameObject;
                windScript = newWind.GetComponent<Wind>();
                windScript.BlowLeft();
            }

            if (rb.velocity.x > 0f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                playerState = StateList.braking;
                Debug.Log("Braking");
            }
            if (rb.velocity.x < 0f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                playerState = StateList.braking;
            }

        }
    }

    //Methods that checks the state of the player for animations
    private void AnimationState()
    {
        if (playerState == StateList.blinking || finishedBlink == false) //check if animation played yet
        {
            finishedBlink = true;
        }
        else if (playerState == StateList.blinking && finishedBlink == true)//
        {
            playerState = StateList.idle;
        }

        else if(playerState == StateList.jumping) //check if state is jumping, if it is it won't run the elseif
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
