using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Wind : MonoBehaviour
{
    float speed = 20f;
    float delayTimer = 1f;

    private Rigidbody2D rb;
    private Animator anim;

    //private GameObject player;
    //public PlayerController pc;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //player = GameObject.FindWithTag("Player");
        //pc = player.GetComponent<PlayerController>();

        //Debug.Log(pc.rb.velocity.x);
        //if(pc.rb.velocity.x > 0)
        //{
        //    BlowRight();
        //}
        //else if(pc.rb.velocity.x < 0)
        //{
        //    BlowLeft();
        //}
    }

    // Update is called once per frame

    public void BlowLeft() //Blow left side
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        StartCoroutine(DelayDestroy());
    }
    public void BlowRight() //Blow right side
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy() //Delay the self destruct.
    {
        yield return new WaitForSeconds(delayTimer);
        Destroy(gameObject);
    }
}
