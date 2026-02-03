using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce4JumpPad : MonoBehaviour
{  
   
    public float jumpPower = 5f; 

    void OnCollisionEnter (Collision collision)
    {
        if (collision.transform.CompareTag("Player")){
            collision.gameObject.GetComponent<Rigidbody>().AddForce(1f, jumpPower, 1f, ForceMode.Impulse);
            
        }
    }
   
}
