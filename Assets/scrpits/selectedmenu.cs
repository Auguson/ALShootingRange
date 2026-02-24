using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectedmenu : MonoBehaviour
{
    public GunSeleceter selector;

    public void OnResumePressed ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("PauseScene");
    }

    public void OnStartPressed ()
    {
        SceneManager.LoadScene(4);
    }public void OnLevelSelect ()
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
        Time.timeScale = 1;
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
        Time.timeScale = 1;
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
    public void OnHTPPressed()
    {
        SceneManager.LoadScene(2);

    }
    
      public void LoadShootingRange()
    {
        SceneManager.LoadScene(10);

    }
    
    

   
      
    
}
