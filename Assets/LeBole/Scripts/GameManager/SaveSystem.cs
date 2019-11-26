using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameBall
{

public class SaveSystem : MonoBehaviour {

	public string NameUser;
	public int eCoin;
	public Text TextBlock;
	public Text[] GlobalPoints;
	public GameObject MenuObject;
	[HideInInspector] public int checkName;
	public int[] SkinsN,ExtraCabezaN, ExtraPiesN;
	public string PlayerName;

	private string SkinsT, ExtraCabezaT,ExtraPiesT;
	[HideInInspector] public int Dinero;

	//Componentes de otro script
	private MenuInicio MenuInicio;

	void Awake(){
		Dinero = PlayerPrefs.GetInt("PuntosGlobales", 0);

		PlayerName = PlayerPrefs.GetString ("Nombre", "Nombre");
	}

	// Use this for initialization
	void Start () {
		MenuInicio = GetComponent<MenuInicio> ();

		PlayerPrefs.SetInt ("Marcador Actual", 0);

		Dinero = PlayerPrefs.GetInt("PuntosGlobales", 0);

		PlayerName = PlayerPrefs.GetString ("Nombre", "Nombre");

		checkName=PlayerPrefs.GetInt ("CheckName",0);

	}

	// Update is called once per frame
	void Update () {
			Dinero = PlayerPrefs.GetInt("PuntosGlobales", 0);
			GlobalPoints[0].text = Dinero.ToString();
			if (SceneManager.GetActiveScene().name == "MenuInicio"){
					PlayerName = TextBlock.text;
					for(int i=0;i<2;i++){
					GlobalPoints[i].text= Dinero.ToString();
			}
		}
	}
	public void CuadroNombre (){
		if(PlayerName[0]!=' '){
			PlayerPrefs.SetString ("Nombre", PlayerName);
			PlayerPrefs.SetInt ("CheckName", 1);
			MenuInicio.MenuNombre.SetActive(false);
			MenuInicio.MenuObject.SetActive(true);
			checkName = PlayerPrefs.GetInt ("CheckName", 0);
		}

	}
	public void deletepref(){
		Dinero = 0;
		PlayerPrefs.DeleteAll ();
		SceneManager.LoadScene("MenuInicio");
		PlayerPrefs.SetInt("PuntosGlobales",10000);
	}
}
}
