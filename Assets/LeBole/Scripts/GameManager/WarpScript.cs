using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameBall{
public class WarpScript : MonoBehaviour {

		// Use this for initialization
		public GameObject OutDoor;
		public GameObject OutDoorPosition;
		public ParticleSystem FreezePart;
		public GameObject Player;
		private Transform playerT;

		void Start () {
			playerT = Player.GetComponent<Transform>();
		}

		// Update is called once per frame
		void Update () {

		}
		private void OnTriggerEnter(Collider other) {
					OutDoor.SetActive(true);
					playerT.transform.position=OutDoorPosition.transform.position;
					FreezePart.gameObject.transform.position=OutDoorPosition.transform.position;
					FreezePart.gameObject.SetActive(true);
					FreezePart.Play();
		}
	}
}
