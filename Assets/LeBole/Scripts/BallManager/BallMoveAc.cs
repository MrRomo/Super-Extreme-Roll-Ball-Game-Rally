
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace GameBall
{


public class BallMoveAc : MonoBehaviour
{
    //variables de propiedades
    public bool m_UseTorque = true; // Whether or not to use torque to move the ball.
    public bool m_UseFly = true; // Whether or not to use torque to move the ball.
    public float m_MaxAngularVelocity = 50f; // The maximum velocity the ball can rotate at.
    public float m_JumpPower = 2; // The force added to the ball when it jumps.
    public float CooldownFreezeInicial=1f, CooldownSpeedInicial=5.1f; //tiempo de cooldown del freeze
    public float SlwM; //Multiplicador de movimiento para SlowMotion cuando se presiona V.


    //Variables de objeto player
    public Rigidbody m_Rigidbody;
    public Transform player;
    public Slider PowerSlider;
    public float m_MovePower; // The force added to the ball to move it.

    //Variables de control de estado
    public float Fall;// Posicion en Y, para detectar caidas del escenario
    public ParticleSystem m_FreezeParticles;
    private bool jump; // whether the jump button is currently pressed
    public Text power; //poder con que se mueve la bola

    //Varibles referentes
    private AudioManager AudioManager;
    private BallManager BallManager; // Reference to the ball controller.
    private BallPower BallPower; // Reference to the ball controller.

		[HideInInspector] public Vector3 movev;
		[HideInInspector] public bool CheckJumpB; //Sirve para evitar el OverJump y gastar todos los saltos de una vez
    [HideInInspector] public bool lockMove;   //bloquea el movimiento (FreezeAll) para secuencias cinematicas


    //Controla la carga del freeze
    //private HealtBall healtBall;
    [HideInInspector] public float PwUpF; //fuerza añadida para saltar en el aire
    [HideInInspector] public float vFront, vmove;
    private bool jumpba,jumpba1;
    private bool SecureJump;


    private void Awake()
    {
      BallManager = GetComponent<BallManager>();
      //healtBall = GetComponent<HealtBall>();
      AudioManager = GetComponent<AudioManager>();
      BallPower = GetComponent<BallPower>();
      jumpba = false;
      jumpba1 = false;
			BallPower.BoostSpeed = 1f;
			CheckJumpB = false;
			SecureJump = true;
			lockMove = true;
      m_Rigidbody.constraints = RigidbodyConstraints.None;
    }



    private void Start()
    {
      BallPower = GetComponent<BallPower>();
      m_Rigidbody = GetComponent<Rigidbody>();
      // Set the maximum angular velocity.
      GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
      Fall = 0;
      SlwM = 1;
			BallPower.FPSc = CooldownFreezeInicial;
			BallPower.SPSc = 0;
			BallPower.SPS.text = " ";
      PwUpF = 1;
    }


    private void Update()
    {
      Fall = player.transform.position.y;
      m_MovePower = PowerSlider.value+BallPower.BoostSpeed;
      power.text =  PowerSlider.value.ToString();

      if (Fall <= -2.0000)
      {
           BallManager.Respawn();
      }
      // Store the value of both input axes.
      if (Input.GetKeyDown(KeyCode.C))
      {
          freezeSK();
      }
      if (Input.GetKeyDown(KeyCode.X))
      {
          SpeedSK();
      }

      float h = Input.GetAxis("Horizontal");
      float v = Input.GetAxis("Vertical");

      jump = Input.GetButton("Jump");
      movev = ((vFront+v) * Vector3.forward + (vmove+h) * Vector3.right).normalized;

      if (Input.GetKey(KeyCode.V))
      {
          SlwM = 0.5f;
          m_UseTorque = false;
      }
      else
      {
          SlwM = 1.15f;
          m_UseTorque = true;
      }
      if (Input.GetKeyDown(KeyCode.E)&&(PowerSlider.value<11))
      {
          PowerSlider.value = PowerSlider.value + 1;
      }
      if (Input.GetKeyDown(KeyCode.Q) && (PowerSlider.value>1))
      {
          PowerSlider.value = PowerSlider.value - 1;
      }
		  SecureJump=true;
			if (((Input.GetKeyDown("space")&&(SecureJump))||(jumpba)) && (BallManager.JumpC>0)){
				JumpF ();
			}

    }

    public void moveFront()
    {
        vFront = -10;
    }
    public void moveFrontoff()
    {
        vFront = 0;
    }
    public void moveBack()
    {
        vFront = 10;
    }
    public void moveBackoff()
    {
        vFront = 0;
    }
    public void moveRight()
    {
        vmove = 10;
    }
    public void moveRightoff()
    {
        vmove = 0;
    }
    public void moveLeft()
    {
        vmove = -10;
    }
    public void moveLeftoff()
    {
        vmove = 0;
    }
    public void jumpb()
    {
      jumpba=(!jumpba1) ? true:false;
      jumpba1 = !jumpba1;
    }
  	public void WINBall(){
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			lockMove = false;
		}


    private void FixedUpdate()
		{
		  if (lockMove) {
				Move (movev, jump);
			}
		}

    public void freezeSK()
    {
      if ((BallPower.FPSc <= 0))
      {
          BallPower.FillFreeze.fillAmount = 0;
	        BallPower.FPSc = CooldownFreezeInicial;
          m_MaxAngularVelocity = 0f;
          m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
          m_Rigidbody.constraints = RigidbodyConstraints.None;
          m_MaxAngularVelocity = 50f;
          // Move the instantiated freeze prefab to the tank's position and turn it on.
          m_FreezeParticles.transform.position = transform.position;
          m_FreezeParticles.gameObject.SetActive(true);
          // Play the particle system of the Ball Freeze
          m_FreezeParticles.Play();
      }
    }
    public void SpeedSK()
    {
      if ((BallPower.SPSc <= 0))
      {
        BallPower.FillSpeed.fillAmount = 0;
        BallPower.SPSc = CooldownSpeedInicial;
      }
    }

    public void JumpF(){
			SecureJump = false;
			if (BallManager.toch != 1)
			{
				PwUpF = 1.5f; //aumenta el poder de salto cuando esta en el aire+
			}
			Debug.Log("Presiono jump");
			CheckJumpB = true;  // Sirve para detectar el salto cuando tiene le PwUP para no recuperar saltos infinitos al tocar el suelo
			BallManager.JumpC= BallManager.JumpC - 1;
			m_Rigidbody.AddForce(Vector3.up * (m_JumpPower*PwUpF), ForceMode.VelocityChange);
			BallManager.SetCountJump();
			AudioManager.EngineAudioJump();
			PwUpF = 1;
			BallManager.toch =0;
			jumpba = false;
    }

    private void OnTriggerStay(Collider other){
      if (other.gameObject.CompareTag("PowerWall_R"))
      {
        m_Rigidbody.AddForce(new Vector3(1, 0, 0), ForceMode.VelocityChange);
      }
      if (other.gameObject.CompareTag("PowerWall_L"))
      {
        m_Rigidbody.AddForce(new Vector3(-1, 0, 0), ForceMode.VelocityChange);
      }
      if (other.gameObject.CompareTag("PowerWall_F"))
      {
        m_Rigidbody.AddForce(new Vector3(0, 0, 1), ForceMode.VelocityChange);
      }
    }


    public void Move(Vector3 moveDirection, bool jump)
    {
      // If using torque to rotate the ball...
      if (m_UseTorque)
      {
          // ... add torque around the axis defined by the move direction.
          m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * m_MovePower*SlwM);
      }
      else
      {
          // Otherwise add force in the move direction.
          m_Rigidbody.AddForce(moveDirection * m_MovePower* SlwM);
      }

            // If on the ground and jump is pressed...
			if (((Input.GetKeyDown("space")&&(SecureJump))||(jumpba)) && (BallManager.JumpC>0))
      {
				SecureJump = false; //Bloquea el salto para evitar OverJump
      }
      else
      {
        if (m_UseFly == true)// Permite usar VelocityChange Para moverse mejor en el aire
        {
          m_Rigidbody.AddForce(new Vector3(moveDirection.x*((1* SlwM) / 5), 0, moveDirection.z*((1 * SlwM) / 5)), ForceMode.VelocityChange);
          BallManager.repulse = 1;
        }
      }
    }
  }
}
