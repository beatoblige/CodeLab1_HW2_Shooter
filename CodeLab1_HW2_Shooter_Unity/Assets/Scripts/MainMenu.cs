using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("CurrentLives", 3); // initial health to full scale
        PlayerPrefs.SetInt("CurrentScore", 0); //initial score to zero
        
        SceneManager.LoadScene(firstLevel);
    }

    public void QuitGame()
    {
        Application.Quit(); //won't operate when you're in the editor. 
    }
}
