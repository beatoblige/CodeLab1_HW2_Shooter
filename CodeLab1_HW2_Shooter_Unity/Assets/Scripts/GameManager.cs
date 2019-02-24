using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int currentLives = 3;

    public float respawnTime = 2f;

    public int currentScore;
    private int highScore = 500;
   
    private void Awake()
    {
        instance = this;
       
    }

    void Start()
    {
        UIManager.instance.livesText.text = "x" + currentLives;
        UIManager.instance.scoreText.text = "Score:" + currentScore;
        highScore = PlayerPrefs.GetInt("HighScore"); //sets intial value to the stored high score in PlayerPrefs
        UIManager.instance.HighScoreText.text = "High Score: " + highScore;
    }

    void Update()
    {
        
    }

    public void KillPlayer()
    {
        currentLives--; //always losing one life
        UIManager.instance.livesText.text = "x" + currentLives;

        if (currentLives > 0)
        {
            //
            StartCoroutine(RespawnCo());
        }
        else
        {
            //gameovercode
            UIManager.instance.gameOverScreen.SetActive(true);
            Wave.instance.canSpawnWaves = false;
        }
        
        
    }

    public IEnumerator RespawnCo() //will run in its own section of time. 
    {
        yield return new WaitForSeconds(respawnTime);
        HealthManager.instance.Respawn();

        Wave.instance.ContinueSpawning();


    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        UIManager.instance.scoreText.text = "Score:" + currentScore;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            UIManager.instance.HighScoreText.text = "High Score: " + highScore;
            PlayerPrefs.SetInt("HighSchore", highScore);
        }
    }

   
}

