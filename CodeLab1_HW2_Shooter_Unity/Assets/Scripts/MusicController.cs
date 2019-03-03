using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public static MusicController instance;

    public AudioSource sceneMusic, bossMusic, victoryMusic, gameOverMusic;

    private void Awake()
    {
        instance = this; 
    }


    // Start is called before the first frame update
    void Start()
    {
        sceneMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StopMusic()
    {
        sceneMusic.Stop();
        bossMusic.Stop();
        victoryMusic.Stop();
        gameOverMusic.Stop();
    }



    public void PlayBoss()
    {
        StopMusic();
        bossMusic.Play();
    }
    
    
    
    public void PlayVictory()
    {
        StopMusic();
        victoryMusic.Play();
    }

    public void PlayGameOver()
    {
        StopMusic();
        gameOverMusic.Play();
    }
    
    
    
}



