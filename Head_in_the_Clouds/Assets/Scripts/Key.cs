using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float teleportDistance;
    [SerializeField] private float followSpeed;

    private float originalXPos;
    private float originalYPos;

    private PlayerController pc;
    private Vector3 originalTransform;

    private bool collected = false;
    //private bool doneWithKey = false; // if the key is inserted into the doorway or not

    // Start is called before the first frame update
    void Start()
    {
        originalXPos = transform.position.x;
        originalYPos = transform.position.y;
        originalTransform = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collected = true;
            pc = collision.GetComponent<PlayerController>();
        }
        if (collision.gameObject.tag == "DoorWayToEarth")
        {
            //Destroy the key when collected
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(collected == true)
        {
            if(Vector3.Distance(transform.position, player.transform.position) > teleportDistance)
            {
                //Reset position to player if player blink

                transform.position = player.transform.position;
            }
            else if (pc.playerState == PlayerController.StateList.die)
            {
                collected = false;
                gameObject.transform.position = new Vector3(originalXPos, originalYPos, 0);
            }
            else
            {
                //follow the player
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);
            }
            
            //transform.position = Vector3.Lerp(originalTransform, player.transform.position, Time.time);
        }
        
    }
}
