using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover_Suelo : MonoBehaviour
{
    public float tam_suelo;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //hallamos la distancia entre la cámara y la posición del suelo
        if (tam_suelo <= (cam.transform.position - transform.position).magnitude)
        {

            transform.position = new Vector3(cam.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}

