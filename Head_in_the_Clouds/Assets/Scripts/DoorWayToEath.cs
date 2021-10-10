using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWayToEath : MonoBehaviour
{
    [SerializeField] private int numberOfKeys = 0;
    [SerializeField] private int target = 4;
    public SpriteRenderer winPlaceholder;
    public PlayerController pc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Collectable")
        {
            numberOfKeys++;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(numberOfKeys == target) // win condition
        {
            // Win here, save highscore.
            winPlaceholder.enabled = true;
            pc.gameFinished = true;
        }
    }
}
