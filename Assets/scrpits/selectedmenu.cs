using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectedmenu : MonoBehaviour
{

  

    public void OnStartPressed ()
    {
        SceneManager.LoadScene(5);
    }
    public void OnStartLevel1Pressed ()
    {
        SceneManager.LoadScene(1);
    }
    public void OnStartLevel2Pressed ()
    {
        SceneManager.LoadScene(6);
    }
    public void OnStartLevel3Pressed ()
    {
        SceneManager.LoadScene(7);
    }

    
    public void OnStart1Pressed ()
    {
        SceneManager.LoadScene(9);

    }
    public void OnStart2Pressed ()
    {
        SceneManager.LoadScene(9);
    }
    public void OnGameModeTriggeredPressed ()
    {
        SceneManager.LoadScene(10);
    }
    public void OnStart3Pressed ()
    {
        SceneManager.LoadScene(9);
    }
    public void OnWavesPressed ()
    {
        SceneManager.LoadScene(12);
    }

   
    public void OnLevel1Pressed ()
    {
        SceneManager.LoadScene(1);

    }
    public void OnLevel2Pressed()
    {
        SceneManager.LoadScene(6);
    }
    public void OnLevel3Pressed()
    {
        SceneManager.LoadScene(7);
    }

   

    public void OnCreditsPressed()
    {
        SceneManager.LoadScene(2);
    }
    public void OnBackPressed()
    {
        SceneManager.LoadScene(0);
    }
    public void OnPausePressed()
    {
        SceneManager.LoadScene(3);
    }
    public void OnResetPressed()
    {
        SceneManager.LoadScene("ShootingRange");

    }
    public void OnHTPPressed()
    {
        SceneManager.LoadScene(4);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
      
    
}
