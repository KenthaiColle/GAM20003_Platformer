using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorWayToEath : MonoBehaviour
{
    [SerializeField] private int numberOfKeys = 0;
    [SerializeField] private int target = 4;
    public SpriteRenderer winPlaceholder;
    public PlayerController pc;

    public Text currentKeyAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Collectable")
        {
            numberOfKeys++;
            currentKeyAmount.text = ": " + numberOfKeys;
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
