using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;


namespace GameBall
{
public class WinScreen : MonoBehaviour {


		//variables del canvas
		public GameObject CanvasWin;
		public GameObject player;
		public GameObject GPointsScreen;
		public Image FILLWIN;
		public Animator CameraAnimator;
		public Text Congratulations;
		public Color WinColor, LooseColor;

		public AudioSource MusicAmbient,MusicSpark;

		public Text PuntosNivelT,PuntosNivelTBonus,TotalPuntos; //Muestra los puntos del nivel.

		//Variables de control de puntos
		private int puntosNivel,PuntosScreen,PuntosScreenBonus, SumaPuntos,Bonus_Speed;

		//Referencias de script;
		private LevelManager LevelManager;
		private BallManager BallManager;
		private SaveSystem SaveSystem;

		//Variables de estado de control
		[HideInInspector] public bool CheckWin,secureWin,checkPuntos;
		private int PuntosPercentsBonus,PuntosPercents;

		private float timewin,timeCord,timecordMulti;

	void awake (){
		CameraAnimator=GetComponent<Animator>();
		CameraAnimator.SetBool("MotionWin",false);
		CheckWin = false;
		secureWin = false;
		checkPuntos = true;
		CanvasWin.SetActive(false);
		FILLWIN.fillAmount = 0;
		PuntosScreen = 0;
		timeCord = 0;
		PuntosPercents=0;
		PuntosPercentsBonus=0;
		LevelManager = GetComponent<LevelManager> ();
		SaveSystem= GetComponent<SaveSystem> ();
		BallManager = player.GetComponent<BallManager> ();
	}

	void Start () {
		CanvasWin.SetActive(false);
		GPointsScreen.SetActive(false);
		LevelManager = GetComponent<LevelManager> ();
		SaveSystem= GetComponent<SaveSystem> ();
		BallManager = player.GetComponent<BallManager> ();
		checkPuntos = true;
		CheckWin = false;
		secureWin = false;
		PuntosScreen = 0; //contador ascendente de puntos del canvas screen
		timeCord = 0f; //recoge tiempo prudente antes de sumar un punto al contador del canvas screen
		timecordMulti=0f;
	}

	void Update () {

			if(CheckWin){
					MusicAmbient.volume = 1- FILLWIN.fillAmount;
					MusicSpark.volume= 0f;
			}

			if (secureWin) {
				if (timewin <= 0) {
					CheckWin = true;
					} else {
					timewin -= Time.deltaTime;
				}
			}

			if (CheckWin) {
				timeCord += Time.deltaTime;
				LevelManager.setWinLevel();
				CanvasWin.SetActive(true);
				if(FILLWIN.fillAmount==1){
					GPointsScreen.SetActive (true);
				}
			}
			if(Bonus_Speed<100){
				if (timeCord>=timecordMulti){
					if(PuntosScreen<puntosNivel){
						PuntosScreen += 1;
					}
					if(PuntosScreenBonus<Bonus_Speed){
						PuntosScreenBonus += 1;
						Porcentaje (Bonus_Speed, PuntosScreenBonus);
						timeCord = 0;
					}
					PuntosNivelT.text = PuntosScreen.ToString ();
					TotalPuntos.text = PuntosScreenBonus.ToString ();
				}
			}
			else{
				if (timeCord>=0.001f){
					if(PuntosScreen<puntosNivel){
						PuntosPercents+=1;
						float tempF=((PuntosPercents*puntosNivel)/100);
						PuntosScreen=Mathf.RoundToInt(tempF);;
					}
					if(PuntosScreenBonus<Bonus_Speed){
						PuntosPercentsBonus += 1;
						float tempF=((PuntosPercentsBonus*Bonus_Speed)/100);
						PuntosScreenBonus= Mathf.RoundToInt(tempF);
						Porcentaje (Bonus_Speed, PuntosScreenBonus);
						timeCord = 0;
					}
					if(PuntosScreenBonus>Bonus_Speed){
						PuntosScreenBonus=Bonus_Speed;
					}
					PuntosNivelT.text = PuntosScreen.ToString ();
					TotalPuntos.text = PuntosScreenBonus.ToString ();
				}
			}
		}

		public void SetWin(){
			timewin = 5f; //Establece un tiempo prudente antes de mostrar el canvas win.
			secureWin = true;
			puntosNivel = BallManager.Count;
			float BonSpeed= (((PlayerPrefs.GetInt("TopSpeed",0))/100f)*10f)*puntosNivel;
			if (PlayerPrefs.GetInt("TopSpeed",0)>PlayerPrefs.GetInt(SceneManager.GetActiveScene ().name,0)){
				PlayerPrefs.SetInt(SceneManager.GetActiveScene ().name,PlayerPrefs.GetInt("TopSpeed",0));
			}
			Bonus_Speed = Mathf.RoundToInt(BonSpeed);
			Debug.Log("Puntos Bonus/Nivel" +(PlayerPrefs.GetInt("TopSpeed",0)) +"/" + puntosNivel + "Total " + Bonus_Speed);
			PuntosNivelTBonus.text = "x"+(((PlayerPrefs.GetInt("TopSpeed",0))/100f)*10f) +"\n Max. Velocidad: "+ PlayerPrefs.GetInt("TopSpeed",0)+"Km/h";
			SaveSystem.Dinero = puntosNivel; //obtiene los puntos del nivel
			timecordMulti=1f/(10f*Bonus_Speed);
			Debug.Log("Timecord en : " + timecordMulti);
			string nombre = PlayerPrefs.GetString ("Nombre", "Nombre");
			Congratulations.text="Felicidades " + nombre;
			Debug.Log("Nombre: " + Congratulations.text + "\n Puntos antes:" + PlayerPrefs.GetInt("PuntosGlobales", 0));
			PlayerPrefs.SetInt ("PuntosGlobales",(PlayerPrefs.GetInt("PuntosGlobales", 0)+Bonus_Speed));
			Debug.Log("Puntos despues: " + PlayerPrefs.GetInt("PuntosGlobales", 0));
		}

		public void SetLoose(){
			FILLWIN.color= LooseColor;
			timewin = 1f; //Establece un tiempo prudente antes de mostrar el canvas win.
			secureWin = true;
			puntosNivel = BallManager.Count;
			float BonSpeed= (((PlayerPrefs.GetInt("TopSpeed",0))/100f)*10f)*puntosNivel;
			Bonus_Speed = Mathf.RoundToInt(BonSpeed);
			Debug.Log("Puntos Bonus/Nivel" +(PlayerPrefs.GetInt("TopSpeed",0)) +"/" + puntosNivel + "Total " + Bonus_Speed);
			PuntosNivelTBonus.text = "x"+(((PlayerPrefs.GetInt("TopSpeed",0))/100f)*10f) +"\n Max. Velocidad: "+ PlayerPrefs.GetInt("TopSpeed",0)+"Km/h";
			SaveSystem.Dinero = puntosNivel; //obtiene los puntos del nivel
			timecordMulti=1f/(10f*Bonus_Speed);
			Debug.Log("Timecord en : " + timecordMulti);
			//CameraAnimator.SetBool("MotionWin",true);
			string nombre = PlayerPrefs.GetString ("Nombre", "Nombre");
			Congratulations.text="Perdiste " + nombre;
			Debug.Log("Nombre: " + Congratulations.text);
		}

		public void StopAnimation(){
			CameraAnimator.SetBool("MotionWin",false);
		}

		public void Porcentaje (int total, int actual){
			float porcentajea= ((100f*actual)/total)/100f;
			//Debug.Log("fillAmount "+porcentajea);
			FILLWIN.fillAmount= porcentajea;
		}





}
}
