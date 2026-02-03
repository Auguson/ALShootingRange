using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectedmenu : MonoBehaviour
{

    public void OnResumePressed ()
    {
       SceneManager.UnloadSceneAsync("PauseScene");
            Cursor.lockState = CursorLockMode.Locked;


    }

    public void OnStartPressed ()
    {
        SceneManager.LoadScene(5);
    }
    public void OnStartLevel1Pressed ()
    {
        SceneManager.LoadScene(10);
    }
    public void OnStartLevel2Pressed ()
    {
        SceneManager.LoadScene(10);
    }
    public void OnStartLevel3Pressed ()
    {
        SceneManager.LoadScene(10);
    }

    
    public void OnStart1Pressed ()
    {
        SceneManager.LoadScene(10);

    }
    public void OnStart2Pressed ()
    {
        SceneManager.LoadScene(11);
    }
    public void OnGameModeTriggeredPressed ()
    {
        SceneManager.LoadScene(4);
    }
    public void OnStart3Pressed ()
    {
        SceneManager.LoadScene(12);
    }
    public void OnWavesPressed ()
    {
        SceneManager.LoadScene(7);
    }

   
    public void OnLevel1Pressed ()
    {
        SceneManager.LoadScene(10);

    }
    public void OnLevel2Pressed()
    {
        SceneManager.LoadScene(11);
    }
    public void OnLevel3Pressed()
    {
        SceneManager.LoadScene(12);
    }

   

    public void OnCreditsPressed()
    {
        SceneManager.LoadScene(3);
    }
    public void OnBackPressed()
    {
        SceneManager.LoadScene(0);
    }
    public void OnPausePressed()
    {
        Debug.Log("Pause");
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }
    public void OnResetPressed()
    {
        SceneManager.LoadScene(1);

    }public void OnAKAPressed()
    {
        SceneManager.LoadScene(10);

    }public void OnRevolverPressed()
    {
        SceneManager.LoadScene(10);

    }public void OnPistolPressed()
    {
        SceneManager.LoadScene(10);

    }
    public void OnHTPPressed()
    {
        SceneManager.LoadScene(2);

    }
    
    

   
      
    
}
