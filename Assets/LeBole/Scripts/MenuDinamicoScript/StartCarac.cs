using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class StartCarac : MonoBehaviour {

	public Slider[] SliderNormal;
	public Slider[] SliderPlus;
	public GameObject[] SliderPlusGO;
	public Text[] TextosStand;
	public string[] PropBall;


	// Use this for initialization
	void Awake () {
		PropBall[0]="MassExtra";
		PropBall[1]="HealtExtra";
		PropBall[2]="ThignessExtra";
		PropBall[3]="DragExtra";
		PropBall[4]="BouncinessExtra";
		PropBall[5]="JumpExtra";
	}
	public void SetStarDefault(){
		PlayerPrefs.SetInt("TryTop", -1);
		PlayerPrefs.SetInt("TryBottom", -1);
		SetDefault();
	}


	public void SetDefault(){
		GameObject Nombre= GameObject.Find("Nombre1");
		Text NombreCarac = Nombre.GetComponent<Text>();
		NombreCarac.text=	PlayerPrefs.GetString("NameBall", "Bola Estandar");

		SetSlider(PlayerPrefs.GetFloat("Mass",1.15f),0);
	  SetSlider(PlayerPrefs.GetFloat("Healt",100f),1);
		SetSlider(PlayerPrefs.GetFloat("Thigness",25f),2);
		SetSlider(PlayerPrefs.GetFloat("Drag",0.2f),3);
		SetSlider(PlayerPrefs.GetFloat("Bounciness",0f),4);
		float JumpProm=(((PlayerPrefs.GetFloat("Jump",7.4f))/(PlayerPrefs.GetFloat("Mass",1.15f))));
		SetSlider(Mathf.Round(JumpProm),5);
		PlayerPrefs.SetInt ("SelecSkinView",PlayerPrefs.GetInt ("SelecSkin", 0));
		PlayerPrefs.SetInt ("SelecExtraCabezaView",	PlayerPrefs.GetInt ("SelecExtraCabeza", 0));
		PlayerPrefs.SetInt ("SelecExtraPiesView", PlayerPrefs.GetInt ("SelecExtraPies", 0));
		PlayerPrefs.SetInt("BallMeshTry", PlayerPrefs.GetInt("BallMesh", 0));

		GameObject ScriptBall;
		BallSkinStart BallSkinStart;
		ScriptBall= GameObject.Find("PlayerStart");
		BallSkinStart = ScriptBall.GetComponent<BallSkinStart>();
		BallSkinStart.SetView();
		SetTry();
	}

	public void SetTry(){
		if(PlayerPrefs.GetInt("TryBottom", 0)>(-1)){
			int codeEx =PlayerPrefs.GetInt("TryBottom", 0);
			SetSliderPlus(PlayerPrefs.GetFloat(PropBall[codeEx]+"Try",0f),codeEx);
		}
		if(PlayerPrefs.GetInt("TryTop",0)>(-1)){
			int codeEx =PlayerPrefs.GetInt("TryTop", 0);
			SetSliderPlus(PlayerPrefs.GetFloat(PropBall[codeEx]+"Try",0f),codeEx);
		}
	}

	public void SetSlider(float valueDef,int codeSlider){
		TextosStand[codeSlider].text= (valueDef).ToString();
		SliderNormal[codeSlider].value=valueDef; //Escribo los valores en los textos de propiedades del Canvas
		SetSliderPlus(0f,codeSlider);
		if(PlayerPrefs.GetFloat(PropBall[codeSlider],0f)!=0f){
			SetSliderPlus(PlayerPrefs.GetFloat(PropBall[codeSlider],0f),codeSlider); //Envio los valores de SliderPricipal al plus
			//Debug.Log(PropBall[codeSlider]+": " + PlayerPrefs.GetFloat(PropBall[codeSlider]));																												 				 	 			//Obteniendo lo que esta guardado en PlayerPrefs EXTRA
		}
	}

	public void SetSliderPlus(float valuePlus,int codeSlider){
		float plus=SliderNormal[codeSlider].value+valuePlus; //Configuro una variable para guardar el valor plus
		SliderPlusGO[codeSlider].SetActive(true);
		if(valuePlus==0f){
			SliderPlus[codeSlider].value=0f;
			SliderPlusGO[codeSlider].SetActive(false);
			TextosStand[codeSlider].text= (SliderNormal[codeSlider].value).ToString();
		}
		else{
			SliderPlus[codeSlider].value=plus;
			TextosStand[codeSlider].text= (valuePlus+SliderNormal[codeSlider].value).ToString();
		}
		if(plus<0){
			SliderPlus[codeSlider].value=0f;
			TextosStand[codeSlider].text="0";
			SliderPlusGO[codeSlider].SetActive(false);
		}
	}
}
