using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAvion : MonoBehaviour 
{
	public Rigidbody rb;
	[Tooltip("Forward Speed")]
    [Range(0, 150)] 	 
	public float forwardSpeed;
	public float heightSpeed;
	public float stopSpeed;
	public float turboSpeed;
	public float yawSpeed;
	public float rollSpeed;

	[HideInInspector]
	public GameMaster gm;

	[Header("Balística")]
	int currentGunPoint = 0;
	public float firingRate = 0.5f;
	float timeToShoot = 0;
	bool isFiring = false;
	// array con dos lugares, de los lugares donde aparecen los disparos de la ametralladora
	public Transform[] gunPoints;
	public GameObject explosion; 
	public Transform spawnMisil;
	public GameObject misil;

	Vector3 pos;
	Vector3 posicionPrevia;

	void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody>();
		float drag = rb.drag;
		//Debug.Log("El script fue agregado a: " + gameObject.name);
	}	

	void FixedUpdate () 
	{

		if (Input.GetKey("r"))
		{
			Debug.Log("Vas a ir para arriba");
			rb.AddForce(new Vector3(0,1,0) * heightSpeed);
		}
		else if (Input.GetKey("f"))
		{
			Debug.Log("Vas a ir para abajo");
			rb.AddForce(new Vector3(0,-1,0) * heightSpeed);
		}
		else if (Input.GetKey("e"))
		{
			//Debug.Log("yaw en dirección horaria");
			gameObject.transform.Rotate(0, 1 * yawSpeed, 0);
		}
		else if (Input.GetKey("q"))
		{
			//Debug.Log("yaw en dirección anti horaria");
			gameObject.transform.Rotate(0, - 1 * yawSpeed, 0);
		}
		else if (Input.GetKey("z"))
		{
			Debug.Log("Estás frenando");
			//gameObject.transform.position -= gameObject.transform.forward * Time.deltaTime * stopSpeed; 
			
			forwardSpeed -= stopSpeed;			
		}
		// else if (Input.GetKeyDown("x")){
		// 	Debug.Log("Freno total");	
		//  	forwardSpeed = 0;		
		// }
		else if (Input.GetKey("x"))
		{
			Debug.Log("Estás usando la propulsión");
			//gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * turboSpeed;

			forwardSpeed +=turboSpeed; 
		}


		// Uso GetKeyDown para limitar la cantidad de disparos. para al tocar la barra se llame una sola vez
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			// Creo un misil y le paso el spawnpoint y la rotacion
			GameObject instanciaMisil = Instantiate(misil, spawnMisil.position, Quaternion.Euler(90.0f, 0, 0));
			
			// Le estoy agregando la velocidad al misil que tiramos
			//instanciaMisil.GetComponent<Rigidbody>().velocity = rb.velocity * 1.5f);

			instanciaMisil.GetComponent<Rigidbody>().AddForce((pos - posicionPrevia) * 2000f);
		}

		//Esto es para que gire(roll)
		float ejeInvertido = - 1;
		gameObject.transform.Rotate(Input.GetAxis("Vertical"), 0, ejeInvertido * Input.GetAxis("Horizontal") * rollSpeed);
		
		
		//Esto es para que vaya para adelante
		gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * forwardSpeed;

		posicionPrevia = pos;
		pos = transform.position;



		// Esto es para que pierda velocidad cuando sube y gane velocidad cuando baje (dificulta hacer la "vuelta al mundo")
		//forwardSpeed -= transform.forward.y * Time.deltaTime * 20.0f;

		// Velocidad mímina
		if(forwardSpeed < 0) 
		{
			forwardSpeed = 0; 
		}

		// Velocidad máxima
		if(forwardSpeed > 100) 
		{
			forwardSpeed = 100; 
		}
	}
	void Update () 
	{
		// Cuando presiono la tecla k
		if(Input.GetKeyDown(KeyCode.K))
		{
			isFiring = true;
		}
		// Cuando suelto la tecla k
		else if (Input.GetKeyUp(KeyCode.K))
		{
			isFiring = false;
		}
		//Time.time es el tiempo absoluto del juego, es un float que dice en segundos cuando tiempo pasó
		if(isFiring && Time.time > timeToShoot)
		{
			ShootMachineGun();
			// Después de disparar setea el tiempo para disparar al tiempo actual más la separación de un disparo y otro (en segundos)
			timeToShoot = Time.time + firingRate;
		}

	} 

	public void OnCollisionEnter(Collision choque) 
	{
		
		if(choque.gameObject.tag == "Piso")
        {
			gm.RespawnAvion(gameObject);
			Destroy(gameObject);
        }
	}
	public void OnCollisionExit(Collision choque)
	{
		
		if(choque.gameObject.tag == "Piso")
        {
			Debug.Log("Salió de la colisión " + choque.gameObject.tag);
        }
	}

	void ShootMachineGun() 
	{
		// creo una clase RaycastHIt que guarda todos los datos del raycast, hit es el nombre(es el estandar)
		RaycastHit hit;
		
		/* se hace con if para chequear que cuando no choque con nada no haga cuentas sobre vacío ni sobre el objeto anterior
		Raycast(posición, dirección, información del hit, [distancia máxima])
		Con el out pasas directamente la dirección de memoria 
		El raycast devuelve true o false si choca con algo
		*/
		if ( Physics.Raycast(gunPoints[currentGunPoint % 2].position, transform.TransformDirection(Vector3.forward), out hit))
		{
			// instancio una explosion donde choca el rayo. Despues sacar
			Instantiate(explosion, hit.point, Quaternion.identity);

			// cambio de metralleta
			currentGunPoint ++;
			// Debug para ver el rayo blanco en la escena, no en el juego
			Debug.DrawRay(gunPoints[currentGunPoint % 2].position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white, 2.0f);
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
