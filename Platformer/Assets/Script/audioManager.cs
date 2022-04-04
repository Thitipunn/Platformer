using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip coin;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip enemyHit;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip gui;

    [SerializeField] private AudioSource audioSource;
    public static audioManager instance;

    private void Awake()
    {
        instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void playSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void playJump()
    {
        playSound(jump);
    }

    public void playCoin()
    {
        playSound(coin);
    }

    public void playHit()
    {
        playSound(hit);
    }

    public void playEnemyHit()
    {
        playSound(enemyHit);
    }

    public void playWin()
    {
        playSound(win);
    }

    public void playGUI()
    {
        playSound(gui);
    }
}
