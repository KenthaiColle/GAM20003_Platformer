using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    GameController gc;
    private Rigidbody2D rb;
    private Animator anim;

    private float delayTimer = 1.02f;

    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "DeathGround")
        {
            StartCoroutine(Die());
        }
    }
    private IEnumerator Die() //Delay the self destruct.
    {
        pc.playerState = PlayerController.StateList.die;
        anim.SetInteger("state", 4);
        yield return new WaitForSeconds(delayTimer);
        transform.position = gc.lastCheckpointPos; //Change the position to the last checkpoint if the player dies
        rb.velocity = new Vector2(0, 0); //reset velocity;
        pc.playerState = PlayerController.StateList.idle;
        anim.SetInteger("state", 0);
    }
}
