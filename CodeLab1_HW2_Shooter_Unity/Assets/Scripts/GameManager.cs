using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int currentLives = 3;

    public float respawnTime = 2f;

    public int currentScore;
    private int highScore = 500;

    public bool levelEnding;

    private int levelScore;

    public float waitForLevelEnd = 5f;

    public string nextLevel;

    private bool canPause;
   
   
    private void Awake()
    {
        instance = this;
       
    }

    void Start()
    {

        currentLives = PlayerPrefs.GetInt("Current Lives");
        UIManager.instance.livesText.text = "x" + currentLives;
        
        highScore = PlayerPrefs.GetInt("HighScore"); //sets initial value to the stored high score in PlayerPrefs
        UIManager.instance.HighScoreText.text = "HighScore: " + highScore;
        currentScore = PlayerPrefs.GetInt("CurrentScore");// current score should stay constant from level to level 
        UIManager.instance.scoreText.text = "Score:" + currentScore;

        canPause = true;
    }

    void Update()
    {
        if (levelEnding)
        {
            PlayerController.instance.transform.position += new Vector3(PlayerController.instance.boostSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            PauseUnpause();
        }
        
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
            //for the game over screen
            UIManager.instance.gameOverScreen.SetActive(true);
            Wave.instance.canSpawnWaves = false;
            
            MusicController.instance.PlayGameOver();
            PlayerPrefs.SetInt("HighScore", highScore); //storing high score at game over

            canPause = false;

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
        levelScore += scoreToAdd;
        UIManager.instance.scoreText.text = "Score:" + currentScore;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            UIManager.instance.HighScoreText.text = "HighScore: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }


    public IEnumerator EndLevelCo()
    {
        UIManager.instance.levelEndScreen.SetActive(true);
        PlayerController.instance.stopMovement = true;
        levelEnding = true;
        MusicController.instance.PlayVictory();

        canPause = false;
        
        yield return new WaitForSeconds(.5f);

        UIManager.instance.endScreenLevelScore.text = "Level Score" + levelScore;
        UIManager.instance.endScreenLevelScore.gameObject.SetActive(true);
        
        
        
        yield return new WaitForSeconds(.5f);
        
        PlayerPrefs.SetInt("Current Score", currentScore);
        UIManager.instance.endScreenCurrentScore.text = "Total Score" + currentScore;
        UIManager.instance.endScreenCurrentScore.gameObject.SetActive(true);


        if (currentScore == highScore)
        {
            yield return new WaitForSeconds(.5f); //will show new high score in 1/2 sec
            UIManager.instance.highScoreNotice.SetActive(true);
        }
        
        PlayerPrefs.SetInt("HighScore", highScore); //displaying/saving high score at end of level 
        PlayerPrefs.SetInt("Current Lives", currentLives);
        
        yield return new WaitForSeconds(waitForLevelEnd);

        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f; //regular timed movements to game objects
            PlayerController.instance.stopMovement = false;
        }
        
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            Time.timeScale = 0f; //this will get objects to stop moving and freeze everything
            PlayerController.instance.stopMovement = true;
        }
        
    }
  
}

