using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comportamientoSalud : MonoBehaviour
{
    public Transform barraDeVida;
    Transform posicionDeCamara;

    void Start()
    {
        posicionDeCamara = Camera.main.transform;
    }

    void Update()
    {
        //La barra de salud mira a la camara
        transform.LookAt(posicionDeCamara);        
    }

    public void RecibirEstadoVida(float vida)
    {
        barraDeVida.localScale = new Vector3(vida, 1, 1);
    }
}
