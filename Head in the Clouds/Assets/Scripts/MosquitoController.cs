using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoController : MonoBehaviour
{
    public float pingPong;
    public float speed;

    private float originalXPos;
    private Rigidbody2D rb;

    public float delayTimerGo;
    public float delayTimerStop;

    private bool up = true;

    private void Start()
    {
        originalXPos = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (originalXPos <= transform.position.x + 10)
        {
            if (up)
            {
                StartCoroutine(ZigZagRightUp());
            }
            else
            {
                StartCoroutine(ZigZagRightDown());
            }
        }
        else if (originalXPos > transform.position.x - 10)
        {
            if (up)
            {
                StartCoroutine(ZigZagLeftUp());
            }
            else
            {
                StartCoroutine(ZigZagLeftDown());
            }
                
        }
    }


    private IEnumerator ZigZagLeftUp() //Delay the self destruct.
    {
        yield return new WaitForSeconds(delayTimerGo);
        rb.velocity = new Vector3(-speed, speed/2);
        yield return new WaitForSeconds(delayTimerStop);
        rb.velocity = new Vector3(0, 0);
        up = false;
    }
    private IEnumerator ZigZagLeftDown() //Delay the self destruct.
    {
        yield return new WaitForSeconds(delayTimerGo);
        rb.velocity = new Vector3(-speed, -speed / 2);
        yield return new WaitForSeconds(delayTimerStop);
        rb.velocity = new Vector3(0, 0);
        up = true;
    }
    private IEnumerator ZigZagRightUp() //Delay the self destruct.
    {
        yield return new WaitForSeconds(delayTimerGo);
        rb.velocity = new Vector3(speed, speed / 2);
        yield return new WaitForSeconds(delayTimerStop);
        rb.velocity = new Vector3(0, 0);
        up = false;
    }
    private IEnumerator ZigZagRightDown() //Delay the self destruct.
    {
        yield return new WaitForSeconds(delayTimerGo);
        rb.velocity = new Vector3(speed, -speed / 2);
        yield return new WaitForSeconds(delayTimerStop);
        rb.velocity = new Vector3(0, 0);
        up = true;
    }
}
