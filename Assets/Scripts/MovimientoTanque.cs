using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoTanque : MonoBehaviour
{
    public float movementSpeed = 40;

    GameObject avion;

    void Start() 
    {
        avion = GameObject.Find("Avion entero(Clone)");
    }

    void Update()
    {
        if(avion != null) 
        { 
            transform.LookAt(avion.transform);

            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
        else 
        {
            Debug.Log("No se encuentra el avion");
        }
    }
}
