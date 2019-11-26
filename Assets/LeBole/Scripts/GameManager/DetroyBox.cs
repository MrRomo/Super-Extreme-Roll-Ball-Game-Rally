using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameBall {
public class DetroyBox : MonoBehaviour {

	public GameObject DBox;

		void awake(){
			DBox.SetActive (false);
		}
		void OnTriggerEnter(Collider CajaInmovil){
			if (CajaInmovil.gameObject.CompareTag ("Player")) {
				Destroy (gameObject);
				DBox.SetActive (true);
			}
		}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
}