using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearTanques : MonoBehaviour
{

    public int cantidadTanques = 1;
    public float timeGap = 0.1f;
    public GameObject tanque; 

    void Start()
    {
        PrepararTanques();
    }

    public void PrepararTanques() 
    {
        for(int i = 0; i < cantidadTanques; i++) 
        {
            Invoke("CrearTanque", i * timeGap);
        }
    }
    
    void CrearTanque() 
    {
        Instantiate(tanque, transform.position, Quaternion.identity);
    }
}
