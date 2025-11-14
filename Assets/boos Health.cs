using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boosHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public Text healthText; 
    public int currentHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Boss Health: " + currentHealth.ToString();
    }
}
