using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover_camara : MonoBehaviour
{
    public float vel_cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(vel_cam,0.0f));
    }
}
