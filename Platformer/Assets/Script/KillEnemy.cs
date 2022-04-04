using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    [SerializeField] private GameObject puff;

    [SerializeField] private GameObject enemy;
    [SerializeField] private Rigidbody2D rbPlayer;

    private void Awake()
    {
        rbPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 5);
            Instantiate(puff, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(enemy);
            audioManager.instance.playHit();
        }
    }
}
