using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    bool activated = false;
    private Animator anim;

    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            activated = true;
            anim.SetBool("Activated", true);
            gc.lastCheckpointPos = transform.position;
        }
        
    }
    void TransitionFinished()
    {
        anim.SetBool("TransitionFinished", true);
    }
}
