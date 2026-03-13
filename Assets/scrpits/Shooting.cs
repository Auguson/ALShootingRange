using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{   

    public float bulletSpeed = 50f;
    public GameObject bulletPrefab;
    public GameObject FirePre;
    public GameObject killEffectPrefab; 
    public AudioSource Sudio; 
    public int TargetsNotDestroyed;
    public bool rayCastShooting = false;
    public bool isWaveWaveOne = false;
    public bool isWaveWaveTwo = false;
    public bool isWaveThree = false;
    public int DeadButAliveTargets;

    public GunSeleceter gunSelector;
    
    public Animator whatEverIwant = null;

    public Animator[] animators;

    public GunSeleceter gun;

    public GameObject[] guns;

    public timer waveTimer;


    public float shootCoolDown;
    private float shootTimer = 0;

    void Awake() {
        if(isWaveWaveOne || isWaveThree || isWaveWaveTwo) return;
        foreach(var g in guns) g.SetActive(false);

        guns[gunSelector.GetIndex()].SetActive(true);
    }

    void Start ()
    {
        if(isWaveWaveOne || isWaveThree || isWaveWaveTwo) return;

        guns[gun.GetIndex()].SetActive(true);
        foreach(Animator a in animators) {
            if(a.gameObject.activeSelf) {
                Debug.Log(a.gameObject.transform.name + " is active");
                whatEverIwant = a;
            }
        }
        if(whatEverIwant == null) {
            Debug.Log("Assigned");
        }
        else {
            Debug.Log("Not assigned yet");
        }
    }

    void Update()
    {
        if(shootTimer < shootCoolDown) {
            shootTimer += Time.deltaTime;
            return;
        }
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

        if (DeadButAliveTargets <= 0 && isWaveThree)
        {
            waveTimer.SaveTime(3);
            SceneManager.LoadScene(12);
            Cursor.lockState = CursorLockMode.None;

        }  if (DeadButAliveTargets <= 0 && isWaveWaveTwo)
        {
            waveTimer.SaveTime(2);
            SceneManager.LoadScene(9);
            Cursor.lockState = CursorLockMode.None;

        }  if (DeadButAliveTargets <= 0 && isWaveWaveOne)
        {
            waveTimer.SaveTime(1);
            SceneManager.LoadScene(8);
            Cursor.lockState = CursorLockMode.None;

        }
        
        if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(6, LoadSceneMode.Additive);
            Time.timeScale = 0;
        }  
        else if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 0) {
             Time.timeScale = 1;
             SceneManager.UnloadScene(6);
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    public void SetFloat(string KeyName, float Value)
    {
        PlayerPrefs.SetFloat(KeyName, Value);
    }

    public float GetFloat(string KeyName)
    {
        return PlayerPrefs.GetFloat(KeyName);
    }

    void Shoot()
    {

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

        StartCoroutine(PlayAnimtion(whatEverIwant));
        if(bulletPrefab == null) return; 
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.LookAt(Camera.main.transform.forward);
        bullet.GetComponent<Rigidbody>().velocity = (target - transform.position).normalized * bulletSpeed;
    }

    void RayCastShot()
    {
        shootTimer = 0;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit = new RaycastHit();
        RaycastHit[] allHits;
        allHits = Physics.SphereCastAll(ray, 1f);
        StartCoroutine(PlayAnimtion(whatEverIwant));

        foreach(RaycastHit h in allHits) {
            if(h.transform.CompareTag("target") || h.transform.CompareTag("enemy") || h.transform.CompareTag("boos")){
                hit = h;
                break;
            }
        }

        if (allHits.Length > 0 && hit.transform != null)
        {
                 Sudio.Play();

            if (hit.transform.CompareTag("target"))
            {
                PlayKillEffect(hit.transform.position); 
                hit.transform.gameObject.SetActive(false);
                TargetsNotDestroyed--;  
                StartCoroutine(ResetTarget(hit.transform.gameObject, 5f));
                 Sudio.Play(); 
            }
            else if (hit.transform.CompareTag("enemy"))
            {     
                // add a hit effect here
                if(hit.transform.GetComponent<health>().TakeDamage(5)){
                    DeadButAliveTargets--;
                }
                FirePre.SetActive(true);
                 Sudio.Play();
            }
            else if (hit.transform.CompareTag("boos"))
            {
                if(hit.transform.GetComponent<health>().TakeDamage(5)){
                    DeadButAliveTargets--;
                }
                 Sudio.Play();
            }
            else
            {
                Shoot();
            }
        }
    }

    void PlayKillEffect(Vector3 position)
    {
        if (killEffectPrefab != null)
        {
            GameObject effect = Instantiate(killEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }

    IEnumerator PlayAnimtion (Animator auguson) {
        auguson.SetBool("IsShooting", true);
        yield return new WaitForSeconds(0.1f);
        auguson.SetBool("IsShooting", false);

    }


    private IEnumerator ResetTarget(GameObject target, float delay) {
        yield return new WaitForSeconds(delay);
        target.SetActive(true);
    }
}
