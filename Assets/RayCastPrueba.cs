using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPrueba : MonoBehaviour {


	public GameObject SensorForward,SensorBackward,SensorLeft,SensorRigth;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 PlayerPosition = gameObject.transform.position;

		//RayForward
		Vector3 sensorPosition_F = SensorForward.transform.position;
		Vector3 Direccion_F = sensorPosition_F - PlayerPosition;
		Ray RayForward = new Ray(PlayerPosition, sensorPosition_F);
		Debug.DrawRay(RayForward.origin, Direccion_F*1.0f, Color.green);

		//RayBackward
		Vector3 sensorPosition_B = SensorBackward.transform.position;
		Vector3 Direccion_B = sensorPosition_B - PlayerPosition;
		Ray RayBackward = new Ray(PlayerPosition, sensorPosition_B);
		Debug.DrawRay(RayBackward.origin, Direccion_B*1.0f, Color.green);

		//RayLeft
		Vector3 sensorPosition_L = SensorLeft.transform.position;
		Vector3 Direccion_L = sensorPosition_L - PlayerPosition;
		Ray RayLeft = new Ray(PlayerPosition, sensorPosition_L);
		Debug.DrawRay(RayLeft.origin, Direccion_L*1.0f, Color.green);

		//RayRight
		Vector3 sensorPosition_R = SensorRigth.transform.position;
		Vector3 Direccion_R = sensorPosition_R - PlayerPosition;
		Ray RayRigth = new Ray(PlayerPosition, sensorPosition_R);
		Debug.DrawRay(RayRigth.origin, Direccion_R*1.0f, Color.green);


		//Choque SensorForward
		RaycastHit[] hitForward;
		hitForward = Physics.RaycastAll(RayForward);

		if(hitForward.Length>0){
			foreach(RaycastHit f in hitForward){
				GameObject obstaculoF;
				obstaculoF = f.transform.gameObject;
				Vector3 ObstaculoF = obstaculoF.transform.position;
				Vector3 DirecObstaculoF = ObstaculoF- PlayerPosition;
				float distObstaculoF = Vector3.Distance (PlayerPosition, ObstaculoF);
				Debug.Log ("Primer Obstaculo: "+ f.transform.gameObject.name + "a distancia: " + distObstaculoF);
				Debug.DrawRay(RayForward.origin, DirecObstaculoF*1.0f, Color.red);
				break;
			}
		}
		//Choque SensorBackward
		RaycastHit[] hitBackward;
		hitBackward = Physics.RaycastAll(RayBackward);

		if(hitBackward.Length>0){
			foreach(RaycastHit B in hitBackward){
				GameObject obstaculoB;
				obstaculoB = B.transform.gameObject;
				Vector3 ObstaculoB = obstaculoB.transform.position;
				Vector3 DirecObstaculoB = ObstaculoB- PlayerPosition;
				float distObstaculoB = Vector3.Distance (PlayerPosition, ObstaculoB);
				Debug.Log ("Primer Obstaculo: "+ B.transform.gameObject.name + "a distancia: " + distObstaculoB);
				Debug.DrawRay(RayBackward.origin, DirecObstaculoB*1.0f, Color.red);
				break;
			}
		}
		//Choque SensorLeft
		RaycastHit[] hitLeft;
		hitLeft = Physics.RaycastAll(RayLeft);

		if(hitLeft.Length>0){
			foreach(RaycastHit L in hitLeft){
				GameObject obstaculoL;
				obstaculoL = L.transform.gameObject;
				Vector3 ObstaculoL= obstaculoL.transform.position;
				Vector3 DirecObstaculoL = ObstaculoL- PlayerPosition;
				float distObstaculoL = Vector3.Distance (PlayerPosition, ObstaculoL);
				Debug.Log ("Primer Obstaculo: "+ L.transform.gameObject.name + "a distancia: " + distObstaculoL);
				Debug.DrawRay(RayLeft.origin, DirecObstaculoL*1.0f, Color.red);
				break;
			}
		}
		//Choque SensorRigth
		RaycastHit[] hitRigth;
		hitRigth = Physics.RaycastAll(RayRigth);

		if(hitRigth.Length>0){
			foreach(RaycastHit R in hitRigth){
				GameObject obstaculoR;
				obstaculoR = R.transform.gameObject;
				Vector3 ObstaculoR = obstaculoR.transform.position;
				Vector3 DirecObstaculoR = ObstaculoR- PlayerPosition;
				float distObstaculoR = Vector3.Distance (PlayerPosition, ObstaculoR);
				Debug.Log ("Primer Obstaculo: "+ R.transform.gameObject.name + "a distancia: " + distObstaculoR);
				Debug.DrawRay(RayRigth.origin, DirecObstaculoR*1.0f, Color.red);
				break;
			}
		}




	}
}
