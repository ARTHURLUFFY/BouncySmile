using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player.facingRight)
            {
                player.facingRight = false;
            }
            else if (!player.facingRight)
            {
                player.facingRight = true;
            }
        }
    }
}
