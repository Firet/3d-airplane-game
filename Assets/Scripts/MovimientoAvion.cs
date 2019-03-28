﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAvion : MonoBehaviour {

	public Rigidbody rb;
	public float yawSpeed;
	[Tooltip("Esto cambia la velocidad")]
    [Range(0, 150)] 	 
	public float forwardSpeed;
	public float heightSpeed;
	public float stopSpeed;
	public float turboSpeed;
	
	public GameMaster gm;
	
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		float drag = rb.drag;
		Debug.Log("El script fue agregado a: " + gameObject.name);
	}	

	void FixedUpdate () {

		if (Input.GetKey("r")) {
			Debug.Log("Vas a ir para arriba");
			rb.AddForce(new Vector3(0,1,0) * heightSpeed);
		}
		else if (Input.GetKey("f")) {
			Debug.Log("Vas a ir para abajo");
			rb.AddForce(new Vector3(0,-1,0) * heightSpeed);
		}
		else if (Input.GetKey("e")) {
			//Debug.Log("yaw en dirección horaria");
			gameObject.transform.Rotate(0, 1 * yawSpeed, 0);
		}
		else if (Input.GetKey("q")) {
			//Debug.Log("yaw en dirección anti horaria");
			gameObject.transform.Rotate(0, - 1 * yawSpeed, 0);
		}
		else if (Input.GetKey("z")) {
			Debug.Log("Estás frenando");
			//gameObject.transform.position -= gameObject.transform.forward * Time.deltaTime * stopSpeed; 
			
			forwardSpeed -= stopSpeed;			
		}
		// else if (Input.GetKeyDown("x")){
		// 	Debug.Log("Freno total");	
		//  	forwardSpeed = 0;		
		// }
		else if (Input.GetKey("x")) {
			Debug.Log("Estás usando la propulsión");
			//gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * turboSpeed;

			forwardSpeed +=turboSpeed; 

		}

		//Esto es para que gire(roll)
		float ejeInvertido = - 1;
		gameObject.transform.Rotate(Input.GetAxis("Vertical"), 0, ejeInvertido * Input.GetAxis("Horizontal"));
		
		
		//Esto es para que vaya para adelante
		gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * forwardSpeed;

		// Esto es para que pierda velocidad cuando sube y gane velocidad cuando baje (dificulta hacer la "vuelta al mundo")
		//forwardSpeed -= transform.forward.y * Time.deltaTime * 20.0f;

		// Velocidad mímina
		if(forwardSpeed < 35) {
			forwardSpeed = 35; 
		}

		// Velocidad máxima
		if(forwardSpeed > 100) {
			forwardSpeed = 100; 
		}
	}

	public void OnCollisionEnter(Collision choque) {
		
		if(choque.gameObject.tag == "Piso")
        {
			gm.RespawnAvion(gameObject);
			Destroy(gameObject);
        }
	}
	public void OnCollisionExit(Collision choque) {
		
		if(choque.gameObject.tag == "Piso")
        {
			Debug.Log("Salió de la colisión " + choque.gameObject.tag);
        }
	}
}




		//  unity plane tutorial https://www.youtube.com/watch?v=lCulq9J0Y9E&t=43s
		/* 
		Official gta v control settings: 
		W = Throttle up S= Throttle down. A and D is rudder left and right. 
		8 is pitch up, 2 is pitch down, and 4 and 6 are pitch left and right respectively. 
		
		Controles alternativos en este video https://www.youtube.com/watch?time_continue=52&v=j2wPgf71-sE
		
		imagenes para entender manejo aviones
		https://www.google.com/search?q=yaw+meaning&client=firefox-b-d&source=lnms&tbm=isch&sa=X&ved=0ahUKEwit_ZCMtpbgAhXUHbkGHSMZDjgQ_AUIDigB&biw=1366&bih=654#imgrc=-FVlZo-v7Xz-4M:
		*/