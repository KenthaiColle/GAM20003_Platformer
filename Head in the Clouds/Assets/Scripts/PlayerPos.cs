using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    GameController gc;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "DeathGround")
        {
            transform.position = gc.lastCheckpointPos; //Change the position to the last checkpoint if the player dies
            rb.velocity = new Vector2(0, 0); //reset velocity
        }
    }
}
