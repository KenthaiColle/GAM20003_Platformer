using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCloud : MonoBehaviour
{
    [SerializeField] private float lowerCap;
    [SerializeField] private float higherCap;

    [SerializeField] private float moveSpeed;

    [SerializeField] private bool shouldMove = false;

    private Collider2D coll;
    private Rigidbody2D rb;




    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove) //If this cloud should be moving
        {
            //test to see if we are less than the lowerCap
            if (transform.position.y < lowerCap)
            {
                rb.velocity = new Vector2(0, moveSpeed); //Move up if hit limit
            }
            //checks if the cloud is bigger than the higherCap
            if (transform.position.y > higherCap)
            {
                rb.velocity = new Vector2(0, -moveSpeed); //Move downn if hit limit
            }

        }
    }
}
