using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillDoor : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
