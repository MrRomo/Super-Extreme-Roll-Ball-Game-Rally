using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
namespace GameBall {
public class ReparingUP : MonoBehaviour {

	public Collider RedCollider;
	public GameObject RedB;
	public float CoolDownReparing;
	public bool cGUP; 					//comprueba la obtencion del objeto
	public float timeGUP;
	private Vector3 offset;
	void Start () {
			timeGUP = CoolDownReparing;
			cGUP = false;
			offset = transform.position;

	}

	// Update is called once per frame
	private void Update () {



			while(cGUP==true){
				timeGUP = timeGUP - Time.deltaTime;
				Debug.Log("Contando =" + timeGUP.ToString());
				break;
			}
			if (timeGUP <= 0) {
				cGUP = false;
				transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
				transform.position = offset;
				Debug.Log("Reparing");
				timeGUP = CoolDownReparing;
			}

	}

	private void OnTriggerEnter(Collider other)
	{
			if (other.gameObject.CompareTag("Player"))
			{
				transform.localScale = new Vector3(0f, 0f, 0f);
				transform.position+= new Vector3(10f, 10f, 10f);
				cGUP = true;
				Debug.Log ("True :'(  dsf " + cGUP.ToString());

			}
	}
}
}
