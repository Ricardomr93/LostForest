using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Generator : MonoBehaviour
{
    public GameObject item_object;
    public float time_min;
    public float time_max;
    private bool first;
    // Start is called before the first frame update
    void Start()
    {
        first = true;
        Gener_item();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void Gener_item()
    {
        if (!first)
        {
            Instantiate(item_object, transform.position, Quaternion.identity);
            var time = Random.Range(time_min, time_max);
            Invoke("Gener_item", time);
        }
        else
        {
            first = false;
            var time = Random.Range(time_min, time_max);
            Invoke("Gener_item", time);

        }
        
        
    }
}
