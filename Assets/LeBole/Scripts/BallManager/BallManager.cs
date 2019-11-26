using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


namespace GameBall
{

    public class BallManager : MonoBehaviour {

        //variables de propiedades de objeto
        GameObject playerObject;
        public Transform player;
        public Rigidbody m_Rigidbody;
        public Material[] BallSkin;
		    public MeshRenderer BallSkinPlayer;
        public GameObject playerg;
        public GameObject[] ExtraC_OBJ, ExtraP_OBJ;
        public MeshFilter MeshBall;
      	public Mesh[] MeshBallPersonaje;

        //Variables de control y calculo
        public int Count;//Lleva la cuenta del puntaje
        public float Life; //numero de vidas restante
        public int toch, temptoch; //cuenta si esta en el suelo - - Define un tiempo para que enemy reste daño al player
        public int JumpC; //cuenta el numero de saltos
        private bool check_PwUP;
        public float repulse; //Fuerza opuesta al vector de movimiento
        public int LifeC;
        public bool startFreeze; //Ejecuta un freeze para evitar el lag del primer uso
        public UnityEvent OnLoose;



        //UI Variables
        public Text CountText;
        public Text CountLife;
        public Text CountJump;
        public Transform[] SpawnPoint; //Contiene los Spawns
        public int[] PointsForSpawn; //
        public Text[] PointsForSpawnAdd; // indica los puntos por spawns a los avisos del nivel
        public int No_Spawns; // contiene el numero de spawns del nivel para la funcion for

        //Referencias a otros scrips
        private AudioManager AudioManager;
        private BallMove BallMove;
        private BallPower BallPower;
        private HealtBall HealtBall; // Reference to the ball controller.






     void Start() {
        SetView ();
        Count = 0;
        startFreeze=false;
        repulse = 1;
        Life = PlayerPrefs.GetInt("LifeLevel", 10)+1;
        SetCountText();
        SetCountLife();
        SetCountJump();
        HealtBall = GetComponent<HealtBall>();
  			AudioManager = GetComponent<AudioManager>();
        BallMove = GetComponent<BallMove>();
        BallPower = GetComponent<BallPower>();
  			temptoch = 40;
  			check_PwUP = false;
        for(int i =0; i<No_Spawns; i++){
          PointsForSpawnAdd[i].text= PointsForSpawn[i].ToString();
        }

      }

		void awake (){
      HealtBall = GetComponent<HealtBall>();
		}

	private void Update(){
	    if (temptoch<40){
		    temptoch += 1;
	    }
	    if (JumpC == 0) {
		    check_PwUP = false;
	    }
        if(!startFreeze){
        Respawn();
        Debug.Log("Respawn¡¡¡");
        startFreeze=true;
        }
        if(Input.GetKeyDown(KeyCode.U)){
        OnLoose.Invoke();
        }
    }

    public void SetView (){
  		for(int i=0; i<6; i++){
  				ExtraP_OBJ[i].SetActive(false);
  				ExtraC_OBJ[i].SetActive(false);
  		}
  		ExtraC_OBJ[PlayerPrefs.GetInt ("SelecExtraCabeza", 0)].SetActive(true);
  		ExtraP_OBJ[PlayerPrefs.GetInt ("SelecExtraPies", 0)].SetActive(true);
      BallSkinPlayer.material = BallSkin [PlayerPrefs.GetInt ("SelecSkin", 0)];
      MeshBall.mesh=MeshBallPersonaje[PlayerPrefs.GetInt("BallMesh", 0)];
  	}

		private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag("PickUp")){
				Destroy(other.gameObject);
        Count = Count + 1;
        SetCountText();
        AudioManager.EngineAudioPickUp();
      }
			if (other.gameObject.CompareTag("PickUp2"))
			{
				Destroy(other.gameObject);
				Count = Count + 5;
				SetCountText();
				AudioManager.EngineAudioPickUp();
			}
      if (other.gameObject.CompareTag("PwUp"))
      {
      	JumpC = JumpC + 1;
      	SetCountJump();
        AudioManager.EngineAudioPwUp();
      	check_PwUP = true;
      }
    }

    private void OnCollisionEnter(Collision other)
		{
			if ((other.gameObject.CompareTag ("Floor"))||(other.gameObject.CompareTag ("Enemy"))){
				if (check_PwUP) {
					toch = 1;
          temptoch=40;
  				if (BallMove.CheckJumpB) {
  					JumpC += 1;
  					BallMove.CheckJumpB = false;
  				}
				}
        else {
          temptoch=40;
					toch = 1;
					if (JumpC == 0) {
						JumpC = 1;
				  }
				}
				SetCountJump ();
				AudioManager.EngineAudioToch ();
			}
		}
		private void OnCollisionStay(Collision other) {
		   if ((other.gameObject.CompareTag ("Enemy"))&&(temptoch==40)) {
				repulse = -40;
				SetCountLife();
				toch = 1;
				LifeC =int.Parse(PlayerPrefs.GetString("Thigness", "25"));
				HealtBall.TakeDamage (LifeC);
				AudioManager.EngineAudioHurt ();
			  temptoch=0;
			}
    }

    public void SetCountText()
    {
        CountText.text = "Score : " + Count.ToString();
    }

    public void SetCountLife()
    {
        CountLife.text = "Life: " + Life.ToString();
    }
    public void SetCountJump()
    {
        CountJump.text = JumpC.ToString();
    }
    public void Respawn()
    {

      if(Life==0){
        OnLoose.Invoke();
      }
      if(Life>0){
      Life = Life - 1;
      SetCountLife();
        for(int i=0; i<=No_Spawns; i++){    //Funcion for de los Spawns ¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡
          //Spawn inicial
          if (i==0){
            if (Count < PointsForSpawn[0]){
              transform.position = SpawnPoint[0].transform.position;
              LifeC = 0;
              playerg.gameObject.SetActive(true);
              Debug.Log("Spawn Inicial");
            }
          }
          //Spawns Intermedios
          if((i>0)&&(i<No_Spawns)){
            if((Count >= PointsForSpawn[i-1]) && (Count < PointsForSpawn[i])){
              transform.position = SpawnPoint[i].transform.position;
              LifeC = 0;
              playerg.gameObject.SetActive(true);
              Debug.Log("Spawn Intermedios");
            }
          }
          //SpawnFinal
          if(i==No_Spawns){
            if ((Count >= PointsForSpawn[i-1])){
              transform.position = SpawnPoint[i].transform.position;
              LifeC = 0;
              playerg.gameObject.SetActive(true);
              Debug.Log("Spawn Final");
            }
          }
        }
      repulse = 1;
      HealtBall.m_CurrentHealth = HealtBall.m_StartingHealth;
      HealtBall.SetHealthUI();
      m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
      m_Rigidbody.constraints = RigidbodyConstraints.None;
      //RESTAURA LOS POWERS
      resetPowers();
      BallMove.freezeSK();
      BallPower.setFreeze();
      resetPowers();
      temptoch=40;
      }
    }

  public void resetPowers(){
      BallPower.FillFreeze.fillAmount=1;
      BallPower.FPSc = 0f;
      BallPower.FPS.text = " ";
      BallPower.FillSpeed.fillAmount=1;
      BallPower.SPSc = 0f;
      BallPower.SPS.text = " ";
      BallPower.BoostSpeed = 0;
      BallMove.m_MaxAngularVelocity = 50;
      BallPower.SpeedLight.SetActive(false);
    }
  }
}
