using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoMisil : MonoBehaviour
{
    //lista de gameobjects a destruir, por ahora son solo los tanques

    List<GameObject> tanques;


    private void Start()
    {
        tanques = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)    
    {
        Debug.Log("Misil detonado");
        Destroy(gameObject);

        //forEach y llamar funcion que haga daño
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemigo")) 
        {
            tanques.Add(other.gameObject);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemigo")) 
        {
            tanques.Remove(other.gameObject);
        }    
    }
}
