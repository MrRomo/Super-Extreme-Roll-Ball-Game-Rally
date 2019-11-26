
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace GameBall
{


public class BallMove : MonoBehaviour
{
    //variables de propiedades
    public bool m_UseTorque = true; // Whether or not to use torque to move the ball.
    public bool m_UseFly = true; // Whether or not to use torque to move the ball.
    public float m_MaxAngularVelocity = 50f; // The maximum velocity the ball can rotate at.
    public float JumpPower; // The force added to the ball when it jumps.
    public float CooldownFreezeInicial, CooldownSpeedInicial; //tiempo de cooldown del freeze
    public float SlwM; //Multiplicador de movimiento para SlowMotion cuando se presiona V.
    public Text VelocityText;
    public Image TacometroFillImage,TacometroFillImagePlus;
    public float CenterW;
    public Image ButtonC;


    //Variables de objeto player
    public Collider playerCol;
    public Rigidbody m_Rigidbody;
    public Transform player;
    public Slider PowerSlider;
    public float m_MovePower, m_MovePowerPlus; // The force added to the ball to move it.
    public ParticleSystem SpeedPlusParticles;
    public GameObject GlowYellowSpeed;
    public PhysicMaterial BallMaterial;

    //Variables de control de estado
    public float Fall;// Posicion en Y, para detectar caidas del escenario
    public Text power; //poder con que se mueve la bola
    public GameObject PublicFall;
    
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
    public int topSpeed;
    private float DragBall;



    private void Awake()
    {
      BallManager = GetComponent<BallManager>();
      AudioManager = GetComponent<AudioManager>();
      BallPower = GetComponent<BallPower>();
      jumpba = false;
      jumpba1 = false;
			BallPower.BoostSpeed = 0f;
			CheckJumpB = false;
			SecureJump = true;
			lockMove = true;
      m_Rigidbody.constraints = RigidbodyConstraints.None;
      m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
      m_MovePowerPlus=0f;
      CenterW=player.transform.position.z;
      GlowYellowSpeed.SetActive(false);
      playerCol = GetComponent<Collider>();
      BallMaterial.bounciness = PlayerPrefs.GetFloat("Bounciness", 0.01f)+PlayerPrefs.GetFloat("BouncinessExtra", 0f);;
      BallMaterial.dynamicFriction = PlayerPrefs.GetFloat("Friccion", 0.4f)+PlayerPrefs.GetFloat("FriccionExtra", 0f);
      BallMaterial.staticFriction = BallMaterial.dynamicFriction;
      DragBall= PlayerPrefs.GetFloat("Drag", 0.2f)+PlayerPrefs.GetFloat("DragExtra", 0f);
      m_Rigidbody.mass= PlayerPrefs.GetFloat("Mass", 1.15f)+PlayerPrefs.GetFloat("MassExtra", 0f);
      m_Rigidbody.drag= DragBall;
      JumpPower=  PlayerPrefs.GetFloat("Jump", 7.4f)+ PlayerPrefs.GetFloat("JumpExtra", 0f);
      Debug.Log("Bounciness: "+BallMaterial.bounciness.ToString() + " - Friccion: " +   BallMaterial.dynamicFriction.ToString() +
                " - Mass :" + m_Rigidbody.mass + " Drag: " +  m_Rigidbody.drag.ToString() + " - JumpPower: " +JumpPower.ToString());
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
      int jumpcount =BallManager.JumpC;
      float vel = Mathf.Pow((Mathf.Pow(m_Rigidbody.velocity.x, 2f)+Mathf.Pow(m_Rigidbody.velocity.y, 2f)+Mathf.Pow(m_Rigidbody.velocity.z, 2f)),0.5f);
      Fall = player.transform.position.y;
      m_MovePower = PowerSlider.value+BallPower.BoostSpeed+m_MovePowerPlus;
      power.text = (Math.Round((Mathf.Abs(m_MovePower)),2)).ToString();
      ParticlesSpeed();
      if(Mathf.Abs(vel)>50){
        m_MovePowerPlus=Mathf.Pow(vel,2f)*2f;
        TacometroFillImagePlus.enabled=true;
        m_Rigidbody.angularDrag=0.01f;
        m_Rigidbody.drag=DragBall*(Mathf.Pow(2,(vel*(-0.06f))));
      }
      else{
        if((Mathf.Abs(vel)>25)&&(Mathf.Abs(vel)<=50)){
          m_MovePowerPlus=(vel)*0.9f;
          m_Rigidbody.angularDrag=0.9f;
          m_Rigidbody.drag=DragBall*(Mathf.Pow(2,(vel*(-0.05f))));
        }
        else{
          m_MovePowerPlus=0;
          TacometroFillImagePlus.enabled=false;
          m_Rigidbody.drag=DragBall*(Mathf.Pow(2,(vel*(-0.04f))));
          SpeedPlusParticles.Stop();
        }
      }
      if(Mathf.Abs(vel)>=0){
        TacometroFillImage.fillAmount=(Mathf.Abs(vel)*0.5f)/100;
        TacometroFillImagePlus.fillAmount=(Mathf.Abs(vel)*0.5f)/100;
        VelocityText.text= Math.Round((Mathf.Abs(vel)),2).ToString();
      }
      if(Mathf.Abs(vel)>topSpeed){
        topSpeed=int.Parse((Math.Round(Mathf.Abs(vel))).ToString());
        Debug.Log("TopSpeed " + topSpeed);
        PlayerPrefs.SetInt("TopSpeed", topSpeed);
      }

      
      if((player.transform.position.z<CenterW+0.5f)&&(player.transform.position.z>CenterW-0.5f)){
        ButtonC.color= Color.white;
      }
      else{
        ButtonC.color= Color.red;
      }
      // Store the value of both input axes.
      if (lockMove) {
        if (Fall <= PublicFall.transform.position.y)
        {
            BallManager.Respawn();
        }
                if ((Input.GetKeyDown(KeyCode.C))||(Input.GetKeyDown(KeyCode.JoystickButton3)))
        {
            freezeSK();
        }
        if((Input.GetKeyDown(KeyCode.X))||(Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            SpeedSK();
        }
        if ((Input.GetKeyDown(KeyCode.F))||(Input.GetKeyDown(KeyCode.JoystickButton0))){
          Center();
        }

  			float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // calculate move direction

        movev = ((vFront+v)*Vector3.forward + (vmove+h)*Vector3.right).normalized;

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
        if (((Input.GetKeyDown(KeyCode.E))||(Input.GetKeyDown(KeyCode.JoystickButton7)))&&(PowerSlider.value<11))
        {
            PowerSlider.value = PowerSlider.value + 1;
        }
        if (((Input.GetKeyDown(KeyCode.Q))||(Input.GetKeyDown(KeyCode.JoystickButton6)))&& (PowerSlider.value>1))
        {
            PowerSlider.value = PowerSlider.value - 1;
        }
        SecureJump=true;
  			if ( ( ( ((Input.GetKeyDown("space"))||(Input.GetKeyDown(KeyCode.JoystickButton2)))&&(SecureJump)) ||(jumpba)) &&(jumpcount>0)) {
  				JumpF ();
  			}
      }
    }

    public void Center(){
      transform.position= new Vector3(transform.position.x,transform.position.y,CenterW);
    }
    public void moveFront()
    {
        vFront = -8;
    }
    public void moveFrontoff()
    {
        vFront = 0;
    }
    public void moveBack()
    {
        vFront = 8;
    }
    public void moveBackoff()
    {
        vFront = 0;
    }
    public void moveRight()
    {
        vmove = 8;
    }
    public void moveRightoff()
    {
        vmove = 0;
    }
    public void moveLeft()
    {
        vmove = -8;
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
				Move (movev);
			}
		}

    public void freezeSK()
    {
      if ((BallPower.FPSc <= 0))
      {   BallPower.LockFreezeParticles=true;
          BallPower.FillFreeze.fillAmount = 0;
	        BallPower.FPSc = CooldownFreezeInicial;
          m_MaxAngularVelocity = 0f;
          m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
          m_Rigidbody.constraints = RigidbodyConstraints.None;
          m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
          m_MaxAngularVelocity = 50f;
      }
    }
    public void SpeedSK()
    {
      if ((BallPower.SPSc <= 0))
      {
        BallPower.FillSpeed.fillAmount = 0;
        BallPower.SPSc = CooldownSpeedInicial;
        BallPower.BoostSpeed = 10f;
      }
    }
    public void ParticlesSpeed(){
      SpeedPlusParticles.Play();
      var main = SpeedPlusParticles.main;
      var emission = SpeedPlusParticles.emission;
          main.startSize = (0.17f*(Mathf.Abs(m_Rigidbody.velocity.x)))-4.67f;
      if((m_Rigidbody.velocity.x>40)&&(m_Rigidbody.velocity.x<=50)){
        GlowYellowSpeed.SetActive(false);
        emission.rateOverTime = (99f*(Mathf.Abs(m_Rigidbody.velocity.x)))-3950f;
        GlowYellowSpeed.SetActive(false);
      }
      if((m_Rigidbody.velocity.x>50)&&(m_Rigidbody.velocity.x<70)){
        GlowYellowSpeed.SetActive(false);
        emission.rateOverTime = (99f*(Mathf.Abs(m_Rigidbody.velocity.x)))-3950f;
      }
      if(m_Rigidbody.velocity.x>=70){
        emission.rateOverTime = 1000;
        GlowYellowSpeed.SetActive(true);
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
			// ... add force in upwards.
			m_Rigidbody.AddForce(Vector3.up*(JumpPower*PwUpF), ForceMode.Impulse);
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

		public void Move(Vector3 moveDirection)
        {
            // If using torque to rotate the ball...
            if (m_UseTorque)
            {
                // ... add torque around the axis defined by the move direction.
                m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x)*m_MovePower);
            }
            else
            {
                // Otherwise add force in the move direction.
                m_Rigidbody.AddForce(moveDirection*m_MovePower);
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
			          m_Rigidbody.AddForce(new Vector3(moveDirection.x*((SlwM)/5), 0, moveDirection.z*((SlwM)/5)), ForceMode.VelocityChange);
			          BallManager.repulse = 1;
			        }
			      }

        }



  }
}
