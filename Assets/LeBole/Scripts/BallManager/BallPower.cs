using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace GameBall
{
    public class BallPower : MonoBehaviour
    {
      public Image FillFreeze;                           // The image component of the slider.
      public Image FillSpeed;                            // The image component of the slider.

      public Image JumpImage;
  		public Text FPS, SPS;
  		public float BoostSpeed;
      public GameObject SpeedLight; //Effecto del Speed Power
      public ParticleSystem FreezeParticles;
      [HideInInspector] public bool LockFreezeParticles;

      [HideInInspector] public float currentFreeze, currentSpeed;
      private BallManager BallManager;
      private BallMove BallMove;
      [HideInInspector] public float FPSc,SPSc;
      private float countFreeze, FillSpeedLocal, FillFreezeLocal;

    private void Awake()
    {
      BallManager= GetComponent<BallManager>();
      BallMove= GetComponent<BallMove>();
    }

    void Start()
    {
    	FillSpeedLocal = BallMove.CooldownSpeedInicial;
      FillFreezeLocal= BallMove.CooldownFreezeInicial;
    }

    void Update()
		{
      FillSpeedLocal = BallMove.CooldownSpeedInicial;
      FillFreezeLocal= BallMove.CooldownFreezeInicial;

      if (BallManager.JumpC > 0)
      {
          JumpImage.fillAmount = 1;
      }
      else
      {
          JumpImage.fillAmount = 0;
      }

			if (FPSc > 0)
			{
				FPSc -= Time.deltaTime;
        FillFreeze.fillAmount=1-(FPSc/FillFreezeLocal);
        //FreezePower.value=FillFreeze.fillAmount;
    	  Debug.Log ("Freeze " + FillFreeze.fillAmount.ToString ());
        if(LockFreezeParticles){
        //  SpeedLight.transform.position=transform.position;
        //  SpeedLight.SetActive(true);
          LockFreezeParticles=false;
          setFreeze();
        }
				if (FPSc <= 0) {
					FPS.text = " ";
          LockFreezeParticles=true;
          FreezeParticles.gameObject.SetActive(false);
				} else {
					FPS.text = Math.Round (FPSc, 2).ToString ();
				}
			}

			if (SPSc > 0)
			{
				SPSc -= Time.deltaTime;
				FillSpeed.fillAmount=(1-(SPSc/FillSpeedLocal));
				setSpeed();
        BallMove.BallMaterial.bounciness=0f;
				if (SPSc <= 0) {
					SPS.text = " ";
					BoostSpeed = 0;
					BallMove.m_MaxAngularVelocity = 50;
          SpeedLight.SetActive(false);
          BallMove.BallMaterial.bounciness = (PlayerPrefs.GetFloat("Bounciness",0));
				} else {
					SPS.text = Math.Round (SPSc, 2).ToString ();
          BallMove.m_MaxAngularVelocity = 500;
				}
			}
    }

    public void setFreeze()
    {

        FreezeParticles.transform.position = transform.position;
        FreezeParticles.gameObject.SetActive(true);
        // Play the particle system of the Ball Freeze
        FreezeParticles.Play();
    }

    void setSpeed()
    {
        SpeedLight.SetActive(true);
    }

  }
}
