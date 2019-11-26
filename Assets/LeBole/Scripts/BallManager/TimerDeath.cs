using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBall
{

    public class TimerDeath : MonoBehaviour
    {
        public Transform player;
        public GameObject playerObject;
        public AudioSource Death_Audio;
        public AudioClip DeathAudio;

        private float timeD1;

        // Use this for initialization
        void Start()
        {
            playerObject.GetComponent<HealtBall>();
        }

        private void Update()
        {
            timeD1 = playerObject.GetComponent<HealtBall>().timedeath;

            //Debug.Log("Vivo");
            if (timeD1 > 0f)
            {
                timeD1 -= Time.deltaTime;
                Debug.Log("Cuenta muerto" + timeD1.ToString());
            }
            else
            {
               // Debug.Log("Vivo en else");
                if ((timeD1 < 0.0f) && (playerObject.GetComponent<HealtBall>().m_Dead))
                {
                    Debug.Log("Respawwwwwwwn¡¡¡¡");
                    playerObject.GetComponent<HealtBall>().m_Dead = false;
                    playerObject.GetComponent<BallManager>().Respawn();
                }
            }
            playerObject.GetComponent<HealtBall>().timedeath = timeD1;
        }


        public void EngineAudioDeath()
        {
            Death_Audio.clip = DeathAudio;
            Death_Audio.Play();
        }
    }
}
