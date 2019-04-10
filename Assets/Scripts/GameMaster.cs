using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameMaster : MonoBehaviour
{
    public Transform SpawnPointAvion;
    public GameObject avionPrefab;
    int vidas = 3;

    public CinemachineVirtualCamera cm;
    public GameObject explosion;

    GameObject avionGO;


    void Start()
    {
        CrearAvion(); 
    }

    void CrearAvion() 
    {
        avionGO = Instantiate(avionPrefab, SpawnPointAvion.position, Quaternion.identity);
        avionGO.GetComponent<MovimientoAvion>().gm = this;

             
        cm.Follow = avionGO.transform;
        //Chequear si quiero hacer esto. capaz queda mal
        cm.LookAt = avionGO.transform;        
    }

    // Game Object previo es el avión previo
    public void RespawnAvion(GameObject previo)
    {
        vidas --;
        Instantiate(explosion, previo.transform.position, Quaternion.identity);
        // Me fijo si avión actual es el que esta en el piso o si es uno nuevo (chequear el mayor igual) 
        if (vidas >= 0 && avionGO == previo) 
        {
            //llamamos CancelInvoke para que no llame muchos aviones cuando colisiona dos veces.
            CancelInvoke();
            // Llamo a Crearavion despues de dos segundos para poder ver la explosión
            Invoke("CrearAvion", 1.0f);
        }
        //hacer el else
    }

}
