using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shootingLevel : MonoBehaviour
{
    public float bulletSpeed = 50f;
    public GameObject bulletPrefab;
    public GameObject FireEffect;
    public int TargetsNotDestroyed;
    public bool rayCastShooting = false;
    public int DeadButAliveTargets;

    // Update is called once per frame
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
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
        bullet.transform.LookAt(Camera.main.transform.forward);
        bullet.GetComponent<Rigidbody>().velocity = (target - transform.position).normalized * bulletSpeed;

    }

    void RayCastShot()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("target"))
            {
                Destroy(hit.transform.gameObject);
                TargetsNotDestroyed--;
            }
                else
            {
                    Shoot();

            }
            if (hit.transform.CompareTag("enemy"))
            {
                Destroy(hit.transform.gameObject);
                DeadButAliveTargets--;
            }
                else
            {

            }
            if (hit.transform.CompareTag("boos"))
            {


            }
                else
            { 

            }
        }
    }
}
