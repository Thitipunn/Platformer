using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] private GameObject startgame;
    [SerializeField] private GameObject endgame;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject butt;

    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            butt.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            butt.SetActive(false);
        }
    }

    public void spaceShip()
    {
        if (gamePlay.instance.coin >= 10)
        {
            gameEnd();
        }
        else
        {
            gameStart();
        }
    }

    public void gameEnd()
    {
        panel.SetActive(false);
        endgame.SetActive(true);
        Time.timeScale = 0;   
    }

    public void gameStart()
    {
        panel.SetActive(false);
        startgame.SetActive(true);
        Time.timeScale = 0;
    }
    public void hideMenu()
    {
        audioManager.instance.playGUI();
        startgame.SetActive(false);
        panel.SetActive(true);
        Time.timeScale = 1;
    }
}
