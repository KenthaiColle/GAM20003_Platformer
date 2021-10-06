using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    SpriteRenderer spriteRenderer;

    public Sprite yellowHalo;
    public Sprite lightOrangeHalo;
    public Sprite orangeHalo;
    public Sprite darkOrangeHalo;
    public Sprite redHalo;

    public float playerVel;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerVel = Mathf.Abs(player.velocity.x);
        if (playerVel < 20)
        {
            spriteRenderer.sprite = yellowHalo;
        }
        else if (playerVel >= 20 && playerVel < 40)
        {
            spriteRenderer.sprite = lightOrangeHalo;
        }
        else if (playerVel >= 40 && playerVel < 60)
        {
            spriteRenderer.sprite = orangeHalo;
        }
        else if (playerVel >= 60 && playerVel < 80)
        {
            spriteRenderer.sprite = darkOrangeHalo;
        }
        else if (playerVel >= 80 && playerVel < 100)
        {
            spriteRenderer.sprite = redHalo;
        }
    }
}
