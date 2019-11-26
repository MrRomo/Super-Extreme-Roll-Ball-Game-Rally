using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameBall
{
public class Tutorial : MonoBehaviour {

	public GameObject TutorialPad;
	public GameObject TutorialJump;
	public Text TextPad;

	public float timeTutorial;
	public bool TActive;
	private string NombrePlayerS;

	//Componentes de otro script
	private LevelManager LevelManager;

	// Use this for initialization
	void Start () {
		timeTutorial = 0f;
		TutorialPad.SetActive(false);
		TutorialJump.SetActive(false);
		LevelManager = GetComponent<LevelManager> ();
		NombrePlayerS = PlayerPrefs.GetString("Nombre","Default");
		TActive = LevelManager.active;
	}

	// Update is called once per frame
	void Update () {
		TActive = LevelManager.active;


		if (TActive) {
		timeTutorial += Time.deltaTime;
		}
		LevelManager.canvasG.SetActive(TActive);

		if ((timeTutorial >= 5)&&(timeTutorial <= 6)) {
		TActive = !TActive;
		timeTutorial=7;
		TutorialPad.SetActive(true);
					TextPad.text="Ok "+ NombrePlayerS +" Como ya te has dado \ncuenta, tienes a \ndisposicion una serie \nde botones para " +
					"\nmoverte por el mundo, \npuedes activarlos, preseionando sobre ellos, o simple mente pasando el dedo.";
		}
		if ((timeTutorial >= 9)&&(timeTutorial <=10)) {
			TActive = !TActive;
			timeTutorial=11;
			TutorialJump.SetActive(true);
		}


			if((!TActive)&&(Input.GetKeyDown(KeyCode.KeypadEnter))){
			NextStepTutorial();
		}

		LevelManager.active = TActive;

	}

	public void NextStepTutorial(){
	LevelManager.active = !LevelManager.active;
	TutorialPad.SetActive(false);
	TutorialJump.SetActive(false);
	}



}
	}
