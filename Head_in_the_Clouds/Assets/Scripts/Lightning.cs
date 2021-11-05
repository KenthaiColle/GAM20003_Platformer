using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private Collider2D coll;
    private Animator anim;
    [SerializeField] private AudioSource lightning;

    private float leftAniTime;
    bool coroutineStarted = false;

    [SerializeField] float pauseTimer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //if(coroutineStarted == false)
        //{
        //    StartCoroutine(EnableColl());
        //    coroutineStarted = true;
        //}
        

    }

    public void StartLightning()
    {
        StartCoroutine(EnableColl());
    }

    private IEnumerator EnableColl()
    {
        lightning.Play();
        //Debug.Log("Play Anim");
        coll.enabled = true;
        anim.SetBool("On", true);
        yield return new WaitForSeconds(0.27f);



        anim.SetBool("On", false);
        coll.enabled = false;
        //yield return new WaitForSeconds(pauseTimer); Code before linking it to cloud animation
        //coroutineStarted = false;
    }
}
