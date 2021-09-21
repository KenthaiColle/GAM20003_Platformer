using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Wind : MonoBehaviour
{
    float _speed = 20f;
    float delayTimer = 3f;

    private Rigidbody2D rb;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    public void BlowLeft() //Blow left side
    {
        rb.velocity = new Vector2(-_speed, rb.velocity.y);
        StartCoroutine(DelayDestroy());
    }
    public void BlowRight() //Blow right side
    {
        rb.velocity = new Vector2(_speed, rb.velocity.y);
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy() //Delay the self destruct.
    {
        yield return new WaitForSeconds(delayTimer);
        Destroy(gameObject);
    }
}
