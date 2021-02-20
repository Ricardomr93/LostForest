using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins_Generator : MonoBehaviour
{
    public GameObject coin_object;
    private float ran;
    // Start is called before the first frame update
    void Start()
    {
        ran = Random.Range(1.5f, 4.0f);
        InvokeRepeating("Gener_coin", 0f, ran);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void Gener_coin()
    {
        Instantiate(coin_object, transform.position, Quaternion.identity);
    }
}
