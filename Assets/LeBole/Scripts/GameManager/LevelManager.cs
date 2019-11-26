using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameBall {


public class LevelManager : MonoBehaviour {

	[HideInInspector] public bool active;
		public GameObject canvasG,CanvasGeneral;
		public GameObject canvasP;


	// Use this for initialization


	void Start () {
			active = true;
			canvasG.SetActive(active);
			canvasP.SetActive(!active);
			CanvasGeneral.SetActive(true);
	}

	public void ReCargaNivel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void pause(){
			active = !active;

			canvasG.SetActive (active);
			canvasP.SetActive (!active);
	}

	public void setWinLevel(){
			canvasG.SetActive (false);
			canvasP.SetActive (false);
			active = !active;
	}


	// Update is called once per frame
	void Update () {
			Time.timeScale = (!active) ? 0 : 1f;
			AudioListener.pause = !active;

			if ((Input.GetKeyDown(KeyCode.R))&&(!(SceneManager.GetActiveScene().name == "MenuInicio")))
			{
				ReCargaNivel();
			}

	}
}
}
