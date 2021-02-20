using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Generator : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        var ran = Random.Range(2.0f, 3.0f);
        InvokeRepeating("GenaratorEnemy", 0f, ran);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenaratorEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
