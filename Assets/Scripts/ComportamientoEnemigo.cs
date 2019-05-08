using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoEnemigo : MonoBehaviour
{

    public float vidaRestante = 100;
    float vidaTotal;
    public bool puedeDisparar;
    public float velocidadDisparo = 1;

    public comportamientoSalud csalud;

    public void Start() 
    {
        vidaTotal = vidaRestante;
    }
    public void RecibirDaño(float dañoRecibido) 
    {
        
        vidaRestante -= dañoRecibido;
        if(vidaRestante <= 0) 
        {
            Destroy(gameObject);
        }
        if(csalud) 
        {
            csalud.RecibirEstadoVida(vidaRestante / vidaTotal);
        }
     }
}
