using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoEnemigo : MonoBehaviour
{

    public float vida = 100;
    float vidaTotal;
    public bool puedeDisparar;
    public float velocidadDisparo = 1;

    public comportamientoSalud csalud;

    public void Start() 
    {
        vidaTotal = vida;
    }
    public void RecibirDaño(float dañoRecibido) 
    {
        
        vida -= dañoRecibido;
        if(vida <= 0) 
        {
            Destroy(gameObject);
        }
        if(csalud) 
        {
            csalud.RecibirEstadoVida(vida / vidaTotal);
        }
     }
}
