using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameBall
{
public class MenuPausa : MonoBehaviour {

	private LevelManager LevelManager;

	// Use this for initialization
	void Start () {
		LevelManager = GetComponent<LevelManager> ();
		LevelManager.canvasG.SetActive(LevelManager.active);
		LevelManager.canvasP.SetActive (!LevelManager.active);
	}

	public void SalirPausa(){
			LevelManager.active= !LevelManager.active;
			SceneManager.LoadScene("MenuInicio", LoadSceneMode.Single);
			PlayerPrefs.SetInt("continue", 0);
      Debug.Log("continue? " + PlayerPrefs.GetInt("continue",0));
	}

	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown(KeyCode.P))
		{		LevelManager.active=!LevelManager.active;
				LevelManager.canvasG.SetActive(LevelManager.active);
				LevelManager.canvasP.SetActive (!LevelManager.active);
		}

	}
}
}
