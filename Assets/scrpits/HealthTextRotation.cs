using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthTextRotation : MonoBehaviour
{
    public Camera cam;

    void Awake() {
        if(!cam) {
            cam = Camera.main;
        }
    }

    void Update() {
        if(!cam) {
            cam = Camera.main;
            return;
        }
        transform.LookAt(cam.transform);
        transform.Rotate(0, 180, 0);
    }

}
