/* ﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;


public class MenuDinamicoVertical : MonoBehaviour {
	//InNstancias
	public RectTransform PanelScroll;
	public GameObject ResultadoY;

	//UI
	public Text CuadroOutputResultados;
	public Image ProgressBar;

	//Variables de control y calculo
	private float Scroll=110f;
	private int nResultados;
	private bool bIntanciando;

	//Variables de base de datos
	private string BDFile,connection;

	//public Sprite[] GetSprite;

	public enum TipoItem{
		Skin,
		Extra,
		Personaje
	}
	public TipoItem tipo;

	void Awake () {
		CuadroOutputResultados.text="";
		ProgressBar.fillAmount=0f;
		BDFile ="URI=file:" + Application.dataPath + "/DataBase/Gameball_SQLite.sqlite";
		//GetSprite=Resources.LoadAll<Sprite>("item");

	}

	void Start () {
		CuadroOutputResultados.text="";
		ProgressBar.fillAmount=0f;
		bIntanciando=false;
	}
	public void B_Reload(){
		if(!bIntanciando){
			StartCoroutine(DestruirResultados());
		}
	}
	public void B_Skins(){
		if(!bIntanciando){
			ScrollDownDim(0);
			tipo=TipoItem.Skin;
			string tipoItem=tipo.ToString();
			StartCoroutine(LeerInstanciar(tipoItem));
		}
	}
	public void B_Personaje(){
		if(!bIntanciando){
			ScrollDownDim(0);
			tipo=TipoItem.Personaje;
			string tipoItem=tipo.ToString();
			StartCoroutine(LeerInstanciar(tipoItem));
		}
	}
	public void B_Extra(){
		if(!bIntanciando){
			ScrollDownDim(0);
			tipo=TipoItem.Extra;
			string tipoItem=tipo.ToString();
			StartCoroutine(LeerInstanciar(tipoItem));
		}
	}

	IEnumerator DestruirResultados(){
		if((nResultados!=0)){
			for (int i =0; i<=nResultados; i++){
				Debug.Log("DESTRUYENDO EL " + i);
				Debug.Log("Restantes " + nResultados);

					GameObject ResultadoEncontrado= GameObject.Find("Resultado"+(nResultados-i));
					if(ResultadoEncontrado!=null){
						Destroy(ResultadoEncontrado);
						CuadroOutputResultados.text=(nResultados-i).ToString();
						Porcentaje(nResultados,nResultados-i);
					}
					else if(ResultadoEncontrado==null){
						CuadroOutputResultados.text="";
						Porcentaje(0,nResultados);

					}
					yield return 0;
			}
			//nResultados=0;
			//bIntanciando=false;
			//PanelScroll.offsetMin= new Vector2(8f, 325);
		}
	}

	public void ScrollDownDim(float pScroll){
		PanelScroll.offsetMin= new Vector2(8f, 300f);
		PanelScroll.offsetMin= new Vector2(8f, PanelScroll.offsetMin.y-pScroll);

		//PanelScroll.offsetMax= new Vector2(-30f, PanelScroll.offsetMin.y+pScroll);
		//PanelScroll.offsetMax= new Vector2(PanelScroll.offsetMax.x, PanelScroll.offsetMax.y+pScroll);
	}

	IEnumerator ResultadoInstanciador(string ResName, float pResOffMin, float pResOffMax,string subTipo, string title, int price,string compra, int CodeImag,int CodeObject){
		GameObject ResultadoHijo = Instantiate(ResultadoY) as GameObject;
		ResultadoHijo.transform.SetParent(PanelScroll.transform);
		ResultadoHijo.name= "Resultado" + ResName;
		RectTransform ResultadoPanel = ResultadoHijo.GetComponent<RectTransform>();
		SetValuesPrefabs ResultadoValues = ResultadoHijo.GetComponent<SetValuesPrefabs>();
		ResultadoValues.SetValues(title, price.ToString(),CodeImag,CodeObject,compra,subTipo);

		ResultadoPanel.localScale								= new Vector3(1f,1f,1f);
		ResultadoPanel.anchorMin								= new Vector2(0f,0f);
		ResultadoPanel.anchorMax								= new Vector2(1f 	,1f);
		ResultadoPanel.pivot										= new Vector2(0.5f,0.5f);
		ResultadoPanel.anchoredPosition3D				= new Vector3(0f,0f,0f);

		ResultadoPanel.offsetMin= new Vector2(8, pResOffMin);
		ResultadoPanel.offsetMax= new Vector2(-8, -pResOffMax);
		yield return 0;
	}

	IEnumerator LeerInstanciar(string tipoItem){
		StartCoroutine(DestruirResultados());
		yield return 0;
		int nResultado=0;

		if(!bIntanciando){
		bIntanciando=true;

		using(IDbConnection ConexionBD = new SqliteConnection(BDFile)) //Creamos conexion con base de datos
		{
			Debug.Log("ConexionCreada");
			ConexionBD.Open(); //abrimos la conexion con la base de datos.
			using(IDbCommand comandoBD = ConexionBD.CreateCommand()) //Creamos conexion que va a enviar el comando que consultara en la base de datos
			{
				string consultaSQL = "SELECT rowid, * FROM Items WHERE tipo = '"+ tipoItem + "'"; //Montamos la query
				comandoBD.CommandText = consultaSQL;
				Debug.Log(""+consultaSQL);
				using (IDataReader puntero = comandoBD.ExecuteReader())
				{
					//primera lectura para saber todos los items de la base de datos.
					while(puntero.Read()){
						nResultado++;
						//Debug.Log("nResultados= " + nResultados.ToString());
						yield return puntero;
					}
				}
				int leftINC =0;
				int rightDEC= nResultado-1;
				int numeroResultado = 1;
				//seteamos el PanelScroll
				Debug.Log("nResultados= " + nResultado.ToString());
				ScrollDownDim(Scroll*(nResultado-1));
				//segunda lectura para instanciar los prefabResultados
				using (IDataReader puntero = comandoBD.ExecuteReader())
				{
					while(puntero.Read()){
						if (rightDEC>=0){
							float top =Scroll*rightDEC+10;
							float bottom = Scroll*leftINC+10;
							string regName = (numeroResultado).ToString();

							Porcentaje(nResultado,numeroResultado);
							StartCoroutine(ResultadoInstanciador(regName,top,bottom,puntero.GetString(3),puntero.GetString(4),puntero.GetInt32(5),
																									 puntero.GetString(6),numeroResultado,puntero.GetInt32(8)));
							leftINC++;
							rightDEC--;
							numeroResultado++;
							yield return 0;
							CuadroOutputResultados.text=""+leftINC;
						}
						yield return puntero;
					}
					ConexionBD.Close();
					puntero.Close();
				}
			}
		}
		yield return 0;
		bIntanciando=false;
		nResultados=nResultado;

	}
	}


	public void Porcentaje (int total, int actual){
		float porcentajea= ((100f*actual)/total)/100f;
		//Debug.Log("fillAmount "+porcentajea);
		ProgressBar.fillAmount= porcentajea;
	}
}
*/
