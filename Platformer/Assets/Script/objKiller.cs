using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objKiller : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioManager.instance.playHit();
            gamePlay.instance.gameOver();     
        }
    }
}
