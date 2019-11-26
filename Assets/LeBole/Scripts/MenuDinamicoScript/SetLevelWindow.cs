using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetLevelWindow : MonoBehaviour {

	// Use this for initialization
	public GameObject LevelName, LevelNameS;
	public string lifeNumber, dificulty;
	public Text NivelX, lifeText, VelocityText;
	public Scrollbar FaceLevel;

	public void setWindow(){
		float ValueFace = ((int.Parse(dificulty))*(-0.25f)) +1.25f;
		FaceLevel.value= ValueFace;
		lifeText.text=lifeNumber;
		NivelX.text = LevelName.name;
		LevelNameS.name=LevelName.name;
		PlayerPrefs.SetInt("LifeLevel", int.Parse(lifeNumber));
		VelocityText.text="Vel. Max.: " + PlayerPrefs.GetInt(LevelName.name,0).ToString() + " Km/h";
	}


	void Start () {
	//	Slider.value= 0f;
	}

	// Update is called once per frame
	void Update () {

	}
}
