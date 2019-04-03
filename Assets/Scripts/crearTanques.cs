using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crearTanques : MonoBehaviour
{
    public Transform SpawnPointTanque;
    public GameObject tanquePrefab;
    GameObject tanqueGo;

    void Start()
    {    
        CrearTanque(SpawnPointTanque, 0);
        CrearTanque(SpawnPointTanque, 10);
    }

    void Update()
    {
        
    }

    void CrearTanque(Transform a, int desvio)  {
        //Debug.Log("Funciona CrearTanque");
        //tanqueGo = Instantiate(tanquePrefab, a.position, Quaternion.identity);
        tanqueGo = Instantiate(tanquePrefab, a.position += (Vector3.left * desvio), Quaternion.identity);
    }
}
