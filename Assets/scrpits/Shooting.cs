using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    public float bulletSpeed = 50f;
    public GameObject bulletPrefab;
    public GameObject FirePre;
    public GameObject killEffectPrefab; // <-- Added this
    public int TargetsNotDestroyed;
    public bool rayCastShooting = false;
    public int DeadButAliveTargets;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (rayCastShooting)

            

             RayCastShot();
        }

        if (TargetsNotDestroyed <= 0)
        {
            Debug.Log("You Win!");
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(8);
        }

        if (DeadButAliveTargets <= 0)
        {
            Debug.Log("Wave2");
            SceneManager.LoadScene(11);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(3);
        }   
    }

    void Shoot()
    {
        Debug.Log("Pew! Pew!");

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        Vector3 target;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point;
        }
        else
        {
            target = ray.GetPoint(1000);
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.LookAt(Camera.main.transform.forward);
        bullet.GetComponent<Rigidbody>().velocity = (target - transform.position).normalized * bulletSpeed;
    }

    void RayCastShot()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Handle Target
            if (hit.transform.CompareTag("target"))
            {
                PlayKillEffect(hit.transform.position); // <-- Added effect
                Destroy(hit.transform.gameObject);
                TargetsNotDestroyed--;
            }
            // Handle Enemy
            else if (hit.transform.CompareTag("enemy"))
            {
                PlayKillEffect(hit.transform.position); // <-- Added effect
                Destroy(hit.transform.gameObject);
                DeadButAliveTargets--;
                FirePre.SetActive(true);
            }
            // Handle Boss
            else if (hit.transform.CompareTag("boos"))
            {
                PlayKillEffect(hit.transform.position); // <-- Added effect
                Destroy(hit.transform.gameObject);
                DeadButAliveTargets--;
            }
            else
            {
                Shoot();
            }
        }
    }

    // ?? Creates the kill effect at the point of impact
    void PlayKillEffect(Vector3 position)
    {
        if (killEffectPrefab != null)
        {
            GameObject effect = Instantiate(killEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 2f); // Clean up after 2 seconds
        }
    }
}
