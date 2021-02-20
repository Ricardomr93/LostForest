using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject[] fondos;
    public float[] velocidad_fondo;
    public float[] tamanio;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        MueveFondos();
    }
    private void MueveFondos()
    {
        for (int i = 0; i < fondos.Length; i++)
        {
            if (Mathf.Abs(fondos[i].transform.localPosition.x) > tamanio[i])
            {
                //Regresa el fondo a su posicion original
                fondos[i].transform.localPosition = new Vector3(0.0f, fondos[i].transform.localPosition.y, fondos[i].transform.localPosition.z);
            }
            else
            {
                float offset = Time.deltaTime * velocidad_fondo[i];
                fondos[i].transform.localPosition += new Vector3(offset, 0.0f);
            }
        }
    }
}
