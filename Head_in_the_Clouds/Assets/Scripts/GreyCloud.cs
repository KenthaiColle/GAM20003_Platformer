using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyCloud : MonoBehaviour
{
    [SerializeField] Lightning lightningScript;
    [SerializeField] Animator anim;

    private bool CoroutineRunning = false;

    public float waitTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        lightningScript = GetComponentInChildren<Lightning>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CoroutineRunning == false)
        {
            StartCoroutine(StartAnim());
            CoroutineRunning = true;
        }
        
    }

    void startLightning()
    {
        lightningScript.StartLightning();
    }


    //To delay next lightning
    private IEnumerator StartAnim()
    {
        anim.SetInteger("State", 1);

        yield return new WaitForSeconds(waitTime);

        anim.SetInteger("State", 0);
        CoroutineRunning = false;
    }
}
