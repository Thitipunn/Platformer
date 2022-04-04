using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text textHp;
    [SerializeField] private Text textCoin;
    [SerializeField] private GameObject panel;

    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateHP();
        updateCoin();
        pauseGame();
    }

    public void updateHP()
    {
        textHp.text = PlayerMovement.instance.hp.ToString();
    }

    public void updateCoin()
    {
        textCoin.text = gamePlay.instance.coin.ToString();
    }

    public void showMenu()
    {
        audioManager.instance.playGUI();
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void hideMenu()
    {
        audioManager.instance.playGUI();
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void pauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showMenu();
        }
    }

    public void restartGame()
    {
        gamePlay.instance.gameOver();
    }
}
