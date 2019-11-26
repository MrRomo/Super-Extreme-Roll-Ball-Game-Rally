using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPad : MonoBehaviour {

	public RectTransform PadFlechas, PowersPanel, JumpButton;
	public GameObject PausaPanel, ConfiguracionesPanel,GraficasPanel,ConfigJuegoPanel,SonidoPanel,UnPausePanel,SeguridadPanel;

	private int HandMode;

	// Use this for initialization
	void Start () {
	}
	void Awake () {
		if(0==PlayerPrefs.GetInt("HandMode", 0)){
			RightPadPosition();
		}
		else{
			LeftPadPosition();
		}
	}
	public void OcultAllPanel(){
		PausaPanel.SetActive(false);
		ConfiguracionesPanel.SetActive(false);
		GraficasPanel.SetActive(false);
		ConfigJuegoPanel.SetActive(false);
		SonidoPanel.SetActive(false);
		UnPausePanel.SetActive(false);
		SeguridadPanel.SetActive(false);
	}
	public void ShowSeguridadPanel(){
		OcultAllPanel();
		SeguridadPanel.SetActive(true);
	}
	public void ShowConfiguracionesPanel(){
		OcultAllPanel();
		ConfiguracionesPanel.SetActive(true);
	}
	public void ShowGraficasPanel(){
		OcultAllPanel();
		GraficasPanel.SetActive(true);
	}
	public void ShowConfigJuegoPanel(){
		OcultAllPanel();
		ConfigJuegoPanel.SetActive(true);
	}
	public void ShowPausaPanel(){
		OcultAllPanel();
		PausaPanel.SetActive(true);
		UnPausePanel.SetActive(true);
	}
	public void ShowSonidoPanel(){
		OcultAllPanel();
		SonidoPanel.SetActive(true);
	}


	public void RightPadPosition(){
		PadFlechas.offsetMin= new Vector2(0f,0f);
		PadFlechas.offsetMax= new Vector2(0f,0f);
		PowersPanel.offsetMin=new Vector2(0f,0f);
		PowersPanel.offsetMax=new Vector2(0f,0f);
		JumpButton.offsetMin=new Vector2(0f,0f);
		JumpButton.offsetMax=new Vector2(0f,0f);
		PlayerPrefs.SetInt("HandMode", 0);
	}
	public void LeftPadPosition(){
		PadFlechas.offsetMin= new Vector2(-900f,0f);
		PadFlechas.offsetMax= new Vector2(-900f,0f);
		PowersPanel.offsetMin=new Vector2(900f,0f);
		PowersPanel.offsetMax=new Vector2(900f,0f);
		JumpButton.offsetMin=new Vector2(200f,0f);
		JumpButton.offsetMax=new Vector2(200f,0f);
		PlayerPrefs.SetInt("HandMode", 1);
	}

}
