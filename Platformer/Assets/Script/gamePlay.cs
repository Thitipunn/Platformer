using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamePlay : MonoBehaviour
{
    public int coin = 0;
    public int maxHP = 3;

    public static gamePlay instance;

    private void Awake()
    {
        instance = this;
    }

    public void getCoin(int getCoin)
    {
        coin += getCoin;
    }

    public void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
