using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class enemyAttack : MonoBehaviour
{
    private bool canAttack = true;

    void OnCollisionStay(Collision collision)
    {
        if(!canAttack) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy has attacked the player!");
            // Add attack logic here (e.g., reduce player health)
            if(collision.gameObject.GetComponent<health>() != null)
            {

                collision.gameObject.GetComponent<health>().TakeDamage(5);
                canAttack = false;
                StartCoroutine(AttackCD());
            }
    }

    IEnumerator AttackCD() {
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }
}
}
