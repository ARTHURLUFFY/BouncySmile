using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall2 : MonoBehaviour
{
    public TwoPlayer player;
    public TwoPlayer player2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player.facingRight)
            {
                player.facingRight = false;
                player2.facingRight = true;
            }
            else if (!player.facingRight)
            {
                player.facingRight = true;
                player2.facingRight = false;
            }
        }
    }
}
