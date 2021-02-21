using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eliminator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        Destroy(collision.gameObject, 0.5f);
    }
}
