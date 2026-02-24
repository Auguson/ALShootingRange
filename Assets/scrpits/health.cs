using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class health : MonoBehaviour
{
  public float healthHP;
  public TextMeshProUGUI healthBar;
  public GameObject killEffectPrefab;
  public bool isPlayer;




    void Start() {
      if(healthBar){
            healthBar.text = "HP: " +  healthHP.ToString();
        } 
    }


    public bool TakeDamage (float Damage) {

        healthHP-=Damage;
        if(healthBar){
            healthBar.text = "HP: " +  healthHP.ToString();
        }
        if (healthHP <= 0) { 
            
            PlayKillEffect(transform.position);
            Destroy(gameObject);
            if(isPlayer){
                // unlock mouse here
            Cursor.lockState = CursorLockMode.None;

                SceneManager.LoadScene(11);   
            }
            return true;
            
        }
        return false;

    }

    public float GetHealth () 
    {
        return healthHP;
    } 

    void PlayKillEffect(Vector3 position)
    {
        if (killEffectPrefab != null)
        {
            GameObject effect = Instantiate(killEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }

}
