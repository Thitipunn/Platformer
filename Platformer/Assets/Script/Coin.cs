using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject pop;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gamePlay.instance.getCoin(1);
            Instantiate(pop, new Vector3(transform.position.x, transform.position.y, transform.position.z),Quaternion.identity);
            Destroy(gameObject);
            audioManager.instance.playCoin();
            
        }
    }
}
