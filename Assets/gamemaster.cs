using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemaster : MonoBehaviour
{
    public Transform SpawnPointAvion;
    public GameObject avionPrefab;
    int vidas = 3;

    GameObject avionGO;


    void Start()
    {
        CrearAvion(); 
    }

    void CrearAvion() 
    {
        avionGO = Instantiate(avionPrefab, SpawnPointAvion.position, Quaternion.identity);
        avionGO.GetComponent<MovimientoAvion>().gm = this;
    }

    // Game Object previo es el avión previo
    public void RespawnAvion(GameObject previo)
    {
        vidas --;
        // Me fijo si avión actual es el que esta en el piso o si es uno nuevo
        // chequear el mayor igual 
        if (vidas >= 0 && avionGO == previo) 
        {
            CrearAvion();
        }
        //hacer el else
    }

}
