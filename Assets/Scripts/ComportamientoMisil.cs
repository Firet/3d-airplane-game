using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoMisil : MonoBehaviour
{

    List<GameObject> enemigos;
    [Range(0,200)]
    public float cantidadDaño = 100;

    private void Start()
    {
        enemigos = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)    
    {
        Debug.Log("Misil detonado");
        foreach(GameObject enemigo in enemigos) {
            enemigo.GetComponent<ComportamientoEnemigo>().RecibirDaño(cantidadDaño);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemigo")) 
        {
            enemigos.Add(other.gameObject);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemigo")) 
        {
            enemigos.Remove(other.gameObject);
        }    
    }
}
