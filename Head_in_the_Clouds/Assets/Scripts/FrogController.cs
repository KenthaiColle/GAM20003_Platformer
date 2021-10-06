using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask ground;

    private bool facingLeft = true;

    private Collider2D coll;
    private Rigidbody2D rb;

    float originalXPos;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        originalXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (facingLeft)
        {
            
            //test to see if we are beyond the leftcap
            if (transform.position.x > originalXPos - leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1); //makes the sprite is facing left if it is not.
                }

                //test to see if I am on the ground if so jump
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight); //Jump length is negative because it's going left.
                }

            }
            else
            {
                facingLeft = false;
            }
            //if it is not, we are going to face right.

        }
        else
        {
           
            //test to see if we are beyond the rightcap
            if (transform.position.x < originalXPos + rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1); //makes the sprite is facing right if it is not.
                }


                //test to see if I am on the ground if so jump
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight); //Jump according to jump length and height.
                }

            }
            else
            {
                facingLeft = true;
            }
        }
    }
}
