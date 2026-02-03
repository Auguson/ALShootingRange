using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyfollowplayer : MonoBehaviour
{
    public Transform player;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(!player)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {

            Vector3 dir = (player.position - transform.position);
            dir.y = 0;
            rb.AddForce(dir.normalized * 20f);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 3f);
        
            transform.LookAt(player);
        }
        else Debug.Log("No player found");
    }
}
