using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class enemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy has attacked the player!");
            // Add attack logic here (e.g., reduce player health)
            if(collision.gameObject.GetComponent<health>() != null)
            {

                collision.gameObject.GetComponent<health>().currentHealth -= 5;
            }

            if (collision.gameObject.GetComponent<health>().currentHealth <= 0)
            {

                SceneManager.LoadScene(13);
                Cursor.lockState = CursorLockMode.None;

            }



            else
                Debug.Log("No health component found on " + collision.gameObject.name);
        }
    }
}
