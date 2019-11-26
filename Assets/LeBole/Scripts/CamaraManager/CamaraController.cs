using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBall
{

    public class CamaraController : MonoBehaviour
    {
        public GameObject player;
		    private Vector3 offset;//posicion inicial en la camara
        void Start()
        {
            offset = transform.position - player.transform.position;

        }

        // Update is called once per frame

        void LateUpdate()
        {
					transform.position = player.transform.position + offset;
        }
    }
}
