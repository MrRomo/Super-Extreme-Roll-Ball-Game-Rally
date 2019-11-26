using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using System.Text;
using System.IO;
using Mono.Data.SqliteClient;
public class MenuDinamicoVertical : MonoBehaviour {
	//InNstancias
	public RectTransform PanelScroll;
	public GameObject ResultadoY;

	//UI
	public Text CuadroOutputResultados;
	public Image ProgressBar;
	public Scrollbar ScrollStore;

	//Variables de control y calculo
	private float Scroll=110f;
	private int nResultados;
	private bool bIntanciando;

	//Variables de base de datos
	private string connection;
	private IDbConnection dbCon;
	private IDbCommand dbCom;
	private IDataReader reader;
	private StringBuilder builder;
	private string Tabla;
	private string consultaSQL;
	private string BDataFile;

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
		BDataFile = "/Gameball_SQLite.sqlite";

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
		ScrollStore.value = 1f;
		ScrollStore.size = 0f;
		if(!bIntanciando){
			ScrollDownDim(0);
			tipo=TipoItem.Skin;
			string tipoItem=tipo.ToString();
			consultaSQL = "SELECT rowid, * FROM Skins"; //Montamos la query
			StartCoroutine(LeerInstanciar(tipoItem));
		}
	}
	public void B_Personaje(){
		ScrollStore.value = 1f;
		ScrollStore.size = 0f;
		if(!bIntanciando){
			consultaSQL = "SELECT rowid, * FROM Bolas"; //Montamos la query
			ScrollDownDim(0);
			tipo=TipoItem.Personaje;
			string tipoItem=tipo.ToString();
			StartCoroutine(LeerInstanciar(tipoItem));
		}
	}
	public void B_Extra(){
		ScrollStore.value = 1f;
		ScrollStore.size = 0f;
		if(!bIntanciando){
			ScrollDownDim(0);
			tipo=TipoItem.Extra;
			string tipoItem=tipo.ToString();
			consultaSQL = "SELECT rowid, * FROM Extras"; //Montamos la query
			StartCoroutine(LeerInstanciar(tipoItem));
		}
	}

	IEnumerator DestruirResultados(){
		if((nResultados!=0)){
			for (int i=0; i<=nResultados+1; i++){
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
		PanelScroll.offsetMax= new Vector2(PanelScroll.offsetMax.x, PanelScroll.offsetMax.y);
		//PanelScroll.offsetMax= new Vector2(PanelScroll.offsetMax.x, PanelScroll.offsetMax.y+pScroll);
	}

	IEnumerator InstanciadorSkins(string ResName, float pResOffMin, float pResOffMax,string title, int price){
		GameObject ResultadoHijo = Instantiate(ResultadoY) as GameObject;
		ResultadoHijo.transform.SetParent(PanelScroll.transform);
		ResultadoHijo.name= "Resultado" + (int.Parse(ResName)-1);
		RectTransform ResultadoPanel = ResultadoHijo.GetComponent<RectTransform>();
		SetValuesPrefabs ResultadoValues = ResultadoHijo.GetComponent<SetValuesPrefabs>();
		ResultadoValues.SetValues(title, price.ToString(),(int.Parse(ResName)-1));
		ResultadoPanel.localScale								= new Vector3(1f,1f,1f);
		ResultadoPanel.anchorMin								= new Vector2(0f,0f);
		ResultadoPanel.anchorMax								= new Vector2(1f 	,1f);
		ResultadoPanel.pivot										= new Vector2(0.5f,0.5f);
		ResultadoPanel.anchoredPosition3D				= new Vector3(0f,0f,0f);

		ResultadoPanel.offsetMin= new Vector2(8, pResOffMin);
		ResultadoPanel.offsetMax= new Vector2(-8, -pResOffMax);
		yield return 0;
	}
	IEnumerator InstanciadorExtras(string ResName, float pResOffMin, float pResOffMax,string title,string potenciador,float cantidad, int precio, string SubTipe){
		GameObject ResultadoHijo = Instantiate(ResultadoY) as GameObject;
		ResultadoHijo.transform.SetParent(PanelScroll.transform);
		ResultadoHijo.name= "Resultado" +(int.Parse(ResName)-1);
		RectTransform ResultadoPanel = ResultadoHijo.GetComponent<RectTransform>();
		SetValuesPrefabs ResultadoValues = ResultadoHijo.GetComponent<SetValuesPrefabs>();
		ResultadoValues.SetValuesExtras(title,precio,(int.Parse(ResName)-1),potenciador,cantidad,SubTipe);
		ResultadoPanel.localScale								= new Vector3(1f,1f,1f);
		ResultadoPanel.anchorMin								= new Vector2(0f,0f);
		ResultadoPanel.anchorMax								= new Vector2(1f 	,1f);
		ResultadoPanel.pivot										= new Vector2(0.5f,0.5f);
		ResultadoPanel.anchoredPosition3D				= new Vector3(0f,0f,0f);

		ResultadoPanel.offsetMin= new Vector2(8, pResOffMin);
		ResultadoPanel.offsetMax= new Vector2(-8, -pResOffMax);
		yield return 0;
	}
	IEnumerator InstanciadorPersonaje(string ResName, float pResOffMin, float pResOffMax,string title, int price,
																		float mass, float drag,int healt, int thig, float bounc, float fric, float pJump){

		GameObject ResultadoHijo = Instantiate(ResultadoY) as GameObject;
		ResultadoHijo.transform.SetParent(PanelScroll.transform);
		ResultadoHijo.name= "Resultado" + (int.Parse(ResName)-1);
		RectTransform ResultadoPanel = ResultadoHijo.GetComponent<RectTransform>();
		SetValuesPrefabs ResultadoValues = ResultadoHijo.GetComponent<SetValuesPrefabs>();

		ResultadoValues.SetValuesPersonajes(title,(int.Parse(ResName)-1),price,mass,drag,healt,thig,bounc,fric,pJump);

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
			string filepath = Application.persistentDataPath + "/" + "Gameball_SQLite.sqlite";
			//string filepath = BDataFile;

			Debug.Log("Starting Stablishing connection to:" + connection);
			if (!File.Exists(filepath)){
				WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "Gameball_SQLite.sqlite");
				while(!loadDB.isDone){Debug.Log("Loading LoadDB");}
				File.WriteAllBytes(filepath,loadDB.bytes);
			}
		    connection = "URI=file:" + Application.dataPath + filepath;
			Debug.Log("Stablishing connection to:" + connection);
			dbCon = new SqliteConnection(connection);
			dbCon.Open(); //abrimos la conexion con la base de datos.
			dbCom = dbCon.CreateCommand(); //Creamos conexion que va a enviar el comando que consultara en la base de datos
			dbCom.CommandText = consultaSQL; //Enviamos la Query a la base de datos.
			Debug.Log(""+consultaSQL);
			using (IDataReader puntero = dbCom.ExecuteReader())
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
			using (IDataReader puntero = dbCom.ExecuteReader())
			{
				while(puntero.Read()){
					if (rightDEC>=0){
						float top =Scroll*rightDEC+10;
						float bottom = Scroll*leftINC+10;
						string regName = (numeroResultado).ToString();

						Porcentaje(nResultado,numeroResultado);
						if(tipoItem=="Personaje"){
							StartCoroutine(InstanciadorPersonaje(regName,top,bottom,puntero.GetString(2),
																 puntero.GetInt32(3),puntero.GetFloat(4), puntero.GetFloat(5),puntero.GetInt32(6),
																 puntero.GetInt32(7),puntero.GetFloat(8),puntero.GetFloat(9),puntero.GetFloat(10)));
						}
						if(tipoItem=="Extra"){
							StartCoroutine(InstanciadorExtras(regName,top,bottom,puntero.GetString(2),
															 puntero.GetString(3),puntero.GetFloat(4),puntero.GetInt32(5),puntero.GetString(6)));
						}
            if(tipoItem=="Skin"){
							StartCoroutine(InstanciadorSkins(regName,top,bottom,puntero.GetString(2),puntero.GetInt32(3)));
						}

						leftINC++;
						rightDEC--;
						numeroResultado++;
						yield return 0;
						CuadroOutputResultados.text=""+leftINC;
					}
					yield return puntero;
				}
				dbCon.Close();
				puntero.Close();
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
