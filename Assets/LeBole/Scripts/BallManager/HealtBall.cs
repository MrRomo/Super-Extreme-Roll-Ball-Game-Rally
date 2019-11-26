﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameBall
{


    public class HealtBall : MonoBehaviour
    {

        public float m_StartingHealth;  // The amount of health each tank starts with.
        public Slider m_Slider;                             // The slider to represent how much health the tank currently has.
        public Image m_FillImage;                           // The image component of the slider.
        public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
        public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
        public GameObject m_ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.
        public GameObject player;
        public GameObject playerTimer;
        private BallManager BallManager; // Reference to the ball controller.
        private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
        private ParticleSystem m_ExplosionParticles;        // The particle system the will play when the tank is destroyed.
        public float timedeath;

        [HideInInspector] public float m_CurrentHealth;                      // How much health the tank currently has.
        public bool m_Dead;                                // Has the tank been reduced beyond zero health yet?


        private void Awake()
        {   // Instantiate the explosion prefab and get a reference to the particle system on it.
            m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
            BallManager = GetComponent<BallManager>();
            m_Dead = false;
            m_Slider.maxValue=PlayerPrefs.GetFloat("Healt",100)+PlayerPrefs.GetFloat("HealtExtra",0);
            m_StartingHealth = m_Slider.maxValue;
            Debug.Log("Vida :" + m_Slider.maxValue);
        }
        private void Start()
        {
            m_Slider.maxValue=PlayerPrefs.GetFloat("Healt",100)+PlayerPrefs.GetFloat("HealtExtra",0);
            m_StartingHealth = m_Slider.maxValue;
            timedeath = 0;
            playerTimer.GetComponent<TimerDeath>();
        }

        private void OnEnable()
        {
            // When the tank is enabled, reset the tank's health and whether or not it's dead.
            m_CurrentHealth = m_StartingHealth;
            m_Dead = false;
            timedeath = 0;
            // Update the health slider's value and color.
            SetHealthUI();
        }

        public void TakeDamage(int amount)
        {
            // Reduce current health by the amount of damage done.
            m_CurrentHealth -= amount;

            // Change the UI elements appropriately.
            SetHealthUI();

            // If the current health is at or below zero and it has not yet been registered, call OnDeath.
            BallManager.LifeC = 0;

            if (m_CurrentHealth <= 0f && !m_Dead)
            {
                OnDeath();
            }

        }

        public void SetHealthUI()
        {
            // Set the slider's value appropriately.
            m_Slider.value = m_CurrentHealth;

            // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
            m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
        }


        private void OnDeath()
        {
            // Set the flag so that this function is only called once.
            m_Dead = true;
            //AudioManager.EngineAudioDeath();

            // Move the instantiated explosion prefab to the tank's position and turn it on.
            m_ExplosionParticles.transform.position = transform.position;
            m_ExplosionParticles.gameObject.SetActive(true);

            // Play the particle system of the tank exploding.
            playerTimer.GetComponent<TimerDeath>().EngineAudioDeath();
            m_ExplosionParticles.Play();
            player.gameObject.SetActive(false);
            BallManager.LifeC = 33;
            timedeath = 1f;
         }
    }
}
