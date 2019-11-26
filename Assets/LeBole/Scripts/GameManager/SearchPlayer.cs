using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SearchPlayer : MonoBehaviour{

	public string ID; //En este caso la ID por si hay mas de un NPC
	public float VelMax=10.0f;
	public float VelMin=1.0f;
	public float VelRot=7.0f;
	public GameObject WP,Home,publicfall;

	float velocidadAvance = 8f;
	float distanciaInicial=0;
	float porcentaje =100;
	bool bStored = false;
	bool reachedPoint = false;

	
	void FixedUpdate () {

		if(gameObject.transform.position.y<=(publicfall.transform.position.y)) {
			Destroy(gameObject);
		}

		Vector3 PuntoA = gameObject.transform.position;
		Vector3 PuntoB = WP.transform.position;
		Vector3 HomeP = Home.transform.position;
		Vector3 Direccion = PuntoB - PuntoA;
		Vector3 DireccionHome = HomeP - PuntoA;

		float distancia = Vector3.Distance(PuntoA,PuntoB);
		float distanciaHome = Vector3.Distance(PuntoA,HomeP);

		if((distanciaHome>70)&&(distancia>30)){
			transform.rotation= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(DireccionHome), VelRot*Time.deltaTime);
			transform.position += transform.forward *velocidadAvance*Time.deltaTime;
			if(porcentaje<120 && porcentaje>60 && velocidadAvance<VelMax)
					velocidadAvance+=10.0f*Time.deltaTime;
			if(porcentaje<60 && porcentaje>0 && velocidadAvance> VelMin)
					velocidadAvance -= 10.0f*Time.deltaTime;
			if (distancia<1 && reachedPoint == false){
				reachedPoint=true;
			}

		}
		else{
			if (distanciaInicial!=0) porcentaje = ((distancia*100)/distanciaInicial);

			Ray ray = new Ray (PuntoA,PuntoB);
			Debug.DrawRay(ray.origin, Direccion*1.0f, Color.green);

			if (!bStored){
				distanciaInicial =distancia;
				bStored=true;
			}
			transform.rotation= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direccion), VelRot*Time.deltaTime);
			transform.position += transform.forward *velocidadAvance*Time.deltaTime;
			if(porcentaje<120 && porcentaje>60 && velocidadAvance<VelMax)
					velocidadAvance+=10.0f*Time.deltaTime;
			if(porcentaje<60 && porcentaje>0 && velocidadAvance> VelMin)
					velocidadAvance -= 10.0f*Time.deltaTime;
			if (distancia<1 && reachedPoint == false){
				reachedPoint=true;
			}
		}
	}
}
