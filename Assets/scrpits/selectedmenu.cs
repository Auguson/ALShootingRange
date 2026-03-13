using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class selectedmenu : MonoBehaviour
{
    public GunSeleceter selector;

    public TMP_Text highscoreText;

    public void Start() {
        if(highscoreText) {
            highscoreText.text = PlayerPrefs.GetFloat("highscore").ToString();
        }
    }

    public void OnResumePressed ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("PauseScene");
    }
    
    public void OnGameModeTriggeredPressed ()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
    }

    public void OnBackPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
   
    public void OnPausePressed()
    {
        Debug.Log("Pause");
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }
    
    public void OnAKAPressed()
    {
        SceneManager.LoadScene(10);
        selector.SetIndex(0);


    }
    
    public void OnRevolverPressed()
    {
        SceneManager.LoadScene(10);
    
        selector.SetIndex(2);



    }
    
    public void OnPistolPressed()
    {
        SceneManager.LoadScene(10);
         
        selector.SetIndex(1);
    }
    
    public void LoadSceneByIndex(int index) 
    {
        SceneManager.LoadScene(index);
    }
    
    

   
      
    
}
