using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruir : MonoBehaviour
{

    public bool onStart = true;

    void Start()
    {
        if(onStart)
        {
            Invoke("Destruir", 2.0f);
        }
        
    }

    // La hago publica para que la use la animacion
    public void Destruir() 
    {
        //gameObject es la referencia al gameobject que posee este código (con mayuscula es el tipo de la clase).
        Destroy(gameObject);
    }

}
