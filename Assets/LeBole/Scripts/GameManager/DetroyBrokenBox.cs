using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBall{
public class DetroyBrokenBox : MonoBehaviour {

	// Use this for initialization
	private float setTimeLife;
	private bool collisionB;
	private GameObject SBox;
	
	void OnEnable(){
		setTimeLife = 5f;
		collisionB = true;
		Debug.Log ("Active"+collisionB);
	}

	void Start () {
	}

	// Update is called once per frame
	void Update () {
			
			if (collisionB) {
				
				Debug.Log ("Active"+collisionB);

				if (setTimeLife >= 0) {
					setTimeLife -= Time.deltaTime;
					Debug.Log ("TimeLife:" + setTimeLife);
				} else {
					Destroy (gameObject);
					Debug.Log ("DeathBox");

				}
			}
			else{
				Debug.Log ("NotYet"+collisionB);

			}
	}
}
}