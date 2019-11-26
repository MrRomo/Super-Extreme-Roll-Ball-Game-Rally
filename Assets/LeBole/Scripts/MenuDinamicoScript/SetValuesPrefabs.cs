using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetValuesPrefabs : MonoBehaviour {

	// Use this for initialization

	public Text TituloTexto, PrecioTexto, Masa, Vida, Resistencia, Arrastre, Friccion, Rebote, Name, Jump, ExtraPlus;
	public GameObject CodeObjectOBJ,SubTipe;
	public Button Usar;
	public Button Comprar;
	public Sprite[] SpritesAtlasSkins, SpritesAtlasExtras, SpritesPersonaje;
	public Image SpriteItem;
	public string CBuy;
	public int dinero, precioB;



	//Scrips de referencia
	GameObject ScriptBall;
	BallSkinStart BallSkinStart;
	GameObject Script;
	StartCarac StartCarac;
	//Variables de control

	void Start () {
		Script= GameObject.Find("Textos");
		StartCarac = Script.GetComponent<StartCarac>();
		ScriptBall= GameObject.Find("PlayerStart");
		BallSkinStart = ScriptBall.GetComponent<BallSkinStart>();

	}

	// Update is called once per frame
	void Awake () {
		dinero = PlayerPrefs.GetInt("PuntosGlobales", 0);
		Debug.Log(" Dinero Restante: " + PlayerPrefs.GetInt("PuntosGlobales",0));
	}

	void Update () {

	}

	public void SetValues(string Titulo, string precio, int CodeImag){
		TituloTexto.text=Titulo;
		string itemName = TituloTexto.text;
		TituloTexto.name=Titulo;
		PrecioTexto.text=precio;
		PrecioTexto.name=precio;
		SubTipe.name="Skin";
		CodeObjectOBJ.name=CodeImag.ToString();
		SpriteItem.sprite=SpritesAtlasSkins[(CodeImag)];
		//Debug.Log("CodeImag : " CodeImag);
		CBuy = PlayerPrefs.GetString(TituloTexto.text,"false");
		precioB = int.Parse(PrecioTexto.text);
		if (precioB<=(PlayerPrefs.GetInt("PuntosGlobales",0))) {
			 ColorBlock cb = Comprar.colors;
			 cb.normalColor =  new Color(0.2f, 0.9f, 0.3f, 0.95f);
			 cb.pressedColor =new Color(0.2f, 0.9f, 0.3f, 0.95f);
			 Comprar.colors = cb;
		}
		else{
			ColorBlock cb = Comprar.colors;
			cb.normalColor =  new Color(0.9f, 0.1f, 0.1f, 0.95f);
			cb.pressedColor =new Color(0.9f, 0.1f, 0.1f, 0.95f);
			Comprar.colors = cb;
		}
		if(CBuy == "true") {
			Usar.interactable = true;
			Comprar.interactable = false;
		}
		else if(precioB==0){
			BuyItem();
		}
	}

	public void SetValuesExtras(string Titulo, int precio, int CodeImag, string potenciador, float cantidad, string Subtipe){
		TituloTexto.text=Titulo;
		string itemName = TituloTexto.text;
		TituloTexto.name=Titulo; //Establece el titulo del item extra
		PrecioTexto.text=precio.ToString(); //Guarda el precio de  la base de datos en un GameObject vacio
		PrecioTexto.name=precio.ToString();
		CodeObjectOBJ.name=CodeImag.ToString(); //Guarda el numero del codigo en la base de datos para seleccionar el sprite del item
		SubTipe.name=Subtipe;
		SpriteItem.sprite=SpritesAtlasExtras[CodeImag];
		ExtraPlus.name= potenciador+"Extra";
		ExtraPlus.text= cantidad.ToString();
		CBuy = PlayerPrefs.GetString(TituloTexto.text,"false");
		precioB = int.Parse(PrecioTexto.text);
		if (precioB<=(PlayerPrefs.GetInt("PuntosGlobales",0))) {
			 ColorBlock cb = Comprar.colors;
			 cb.normalColor =  new Color(0.2f, 0.9f, 0.3f, 0.95f);
			 cb.pressedColor =new Color(0.2f, 0.9f, 0.3f, 0.95f);
			 Comprar.colors = cb;
		}
		else{
			ColorBlock cb = Comprar.colors;
			cb.normalColor =  new Color(0.9f, 0.1f, 0.1f, 0.95f);
			cb.pressedColor =new Color(0.9f, 0.1f, 0.1f, 0.95f);
			Comprar.colors = cb;
		}
		if(CBuy == "true") {
			Usar.interactable = true;
			Comprar.interactable = false;
		}
		else if(precioB==0){
			BuyItem();
		}
	}

	public void SetValuesPersonajes(string Titulo,int CodeImag, int Precio,float Mass, float Drag, int Healt, int Thig, float Bounciness, float Fric, float pJump){
		TituloTexto.text=Titulo;
		string itemName = TituloTexto.text;

		TituloTexto.name=Titulo;
		PrecioTexto.text=Precio.ToString();
		PrecioTexto.name=Precio.ToString();
		CodeObjectOBJ.name=(CodeImag).ToString();
		SpriteItem.sprite=SpritesPersonaje[CodeImag];
		SubTipe.name="Personaje";
		CBuy = PlayerPrefs.GetString(TituloTexto.text,"false");
		precioB = int.Parse(PrecioTexto.text);

		if (precioB<=(PlayerPrefs.GetInt("PuntosGlobales",0))) {
			 ColorBlock cb = Comprar.colors;
			 cb.normalColor =  new Color(0.2f, 0.9f, 0.3f, 0.95f);
			 cb.pressedColor =new Color(0.2f, 0.9f, 0.3f, 0.95f);
			 Comprar.colors = cb;
		}
		else{
			ColorBlock cb = Comprar.colors;
			cb.normalColor =  new Color(0.9f, 0.1f, 0.1f, 0.95f);
			cb.pressedColor =new Color(0.9f, 0.1f, 0.1f, 0.95f);
			Comprar.colors = cb;
		}
		if(CBuy == "true") {
			Usar.interactable = true;
			Comprar.interactable = false;
		}
		else if(precioB==0){
			BuyItem();
		}
		Name.text=Titulo;
		Masa.text=Mass.ToString();
		Vida.text=Healt.ToString();
		Resistencia.text= Thig.ToString();
		Arrastre.text=Drag.ToString();;
		Rebote.text=Bounciness.ToString();
		Friccion.text=Fric.ToString();
		Jump.text=pJump.ToString();
		//Guarda los datos en componentes del ResultadoY
	}

	public void BuyItem(){
		CBuy = PlayerPrefs.GetString(TituloTexto.text,"false");
		precioB = int.Parse(PrecioTexto.text);
		dinero = PlayerPrefs.GetInt("PuntosGlobales", 0);
		Debug.Log("Precio: " + precioB +  " Dinero: " + PlayerPrefs.GetInt("PuntosGlobales",0));
		if((precioB<=dinero)&&(CBuy=="false")) {
			int Restante= dinero - precioB;
			PlayerPrefs.SetInt("PuntosGlobales",Restante);
			Usar.interactable = true;
			Comprar.interactable = false;
			PlayerPrefs.SetString(TituloTexto.text,"true");
			CBuy = PlayerPrefs.GetString(TituloTexto.text,"false");
		}
		dinero = PlayerPrefs.GetInt("PuntosGlobales", 0);
		Debug.Log(" Dinero Restante: " + PlayerPrefs.GetInt("PuntosGlobales",0));
	}

	public void UseStore(){

		ScriptBall= GameObject.Find("PlayerStart");
		BallSkinStart = ScriptBall.GetComponent<BallSkinStart>();

		int code = int.Parse(CodeObjectOBJ.name);
		Debug.Log("Selec: " + code);
		if(SubTipe.name=="Skin"){
			PlayerPrefs.SetInt ("SelecSkin", code);
			PlayerPrefs.SetInt ("SelecSkinView",code);
		}
		if(SubTipe.name=="Top"){
			PlayerPrefs.SetInt ("SelecExtraCabeza", code);
			PlayerPrefs.SetInt ("SelecExtraCabezaView",code);
			PlayerPrefs.SetInt("TryTop", -1);
			PlayerPrefs.SetFloat("MassExtra",0f);
			PlayerPrefs.SetFloat("HealtExtra",0f);
			PlayerPrefs.SetFloat("ThignessExtra",0f);
			for(int i=0; i<3;i++){
				StartCarac.SetSliderPlus(0f,i);
			}
			UseExtraPlus();
		}
		if(SubTipe.name=="Bottom"){
			PlayerPrefs.SetInt ("SelecExtraPies", code);
			PlayerPrefs.SetInt ("SelecExtraPiesView",code);
			PlayerPrefs.SetInt("TryBottom", -1);
			PlayerPrefs.SetFloat("DragExtra",0f);
			PlayerPrefs.SetFloat("BouncinessExtra",0f);
			PlayerPrefs.SetFloat("JumpExtra",0f);
			for(int i=3; i<6;i++){
				StartCarac.SetSliderPlus(0f,i);
			}
			UseExtraPlus();
		}
		if(SubTipe.name=="Personaje"){
			PlayerPrefs.SetFloat("Bounciness", float.Parse(Rebote.text));
			PlayerPrefs.SetFloat("Drag", float.Parse(Arrastre.text));
			PlayerPrefs.SetFloat("Healt",float.Parse(Vida.text));
			PlayerPrefs.SetFloat("Mass", float.Parse(Masa.text));
			PlayerPrefs.SetFloat("Thigness", float.Parse(Resistencia.text));
			PlayerPrefs.SetString("NameBall", Name.text);
			PlayerPrefs.SetFloat("Friccion", float.Parse(Friccion.text));
			PlayerPrefs.SetFloat("Jump", float.Parse(Jump.text));
			PlayerPrefs.SetInt("BallMesh", code);
			PlayerPrefs.SetInt("BallMeshTry", code);
			ShowProp();
		}
		BallSkinStart.SetView();

	}

	public void UseExtraPlus(){
		if(ExtraPlus.name!="NOExtra"){
			PlayerPrefs.SetFloat(ExtraPlus.name,float.Parse(ExtraPlus.text));
	 	}
		StartCarac.SetDefault();
	}

	public void TryStore(){
		int code = int.Parse(CodeObjectOBJ.name);
		if(SubTipe.name=="Skin"){
			PlayerPrefs.SetInt("SelecSkinView",code);
		}
		if(SubTipe.name=="Top"){
			for(int i=0; i<3;i++){
				StartCarac.SetSliderPlus(0f,i);
			}
			PlayerPrefs.SetInt("TryBottom", -1);
			StartCarac.SetDefault();
			PlayerPrefs.SetInt("SelecExtraCabezaView",code);
			PlayerPrefs.SetFloat("MassExtraTry",0f);
			PlayerPrefs.GetFloat("HealtExtraTry",0f);
			PlayerPrefs.GetFloat("ThignessExtraTry",0f);
			TryExtraPlus();
		}
		if(SubTipe.name=="Bottom"){
			for(int i=3; i<6;i++){
				StartCarac.SetSliderPlus(0f,i);
			}
			PlayerPrefs.SetInt("TryTop", -1);
			StartCarac.SetDefault();
			PlayerPrefs.SetInt("SelecExtraPiesView",code);
			PlayerPrefs.SetFloat("DragExtraTry",0f);
			PlayerPrefs.SetFloat("BouncinessExtraTry",0f);
			PlayerPrefs.SetFloat("JumpExtraTry",0f);
			TryExtraPlus();
		}
		if(SubTipe.name=="Personaje"){
			PlayerPrefs.SetInt("BallMeshTry", code);
			ShowProp();
		}
		BallSkinStart.SetView();
	}

	public void TryExtraPlus(){
		if(ExtraPlus.name!="NOExtra"){
			PlayerPrefs.SetFloat(ExtraPlus.name+"Try",float.Parse(ExtraPlus.text));
			if(ExtraPlus.name=="MassExtra"){
				PlayerPrefs.SetInt("Try"+SubTipe.name, 0);
				PlayerPrefs.SetInt("TryBottom", -1);
			}
			else if(ExtraPlus.name=="HealtExtra"){
				PlayerPrefs.SetInt("Try"+SubTipe.name, 1);
				PlayerPrefs.SetInt("TryBottom", -1);
			}
			else if(ExtraPlus.name=="ThignessExtra"){
				PlayerPrefs.SetInt("Try"+SubTipe.name, 2);
				PlayerPrefs.SetInt("TryBottom", -1);
			}
			else if(ExtraPlus.name=="DragExtra"){
				PlayerPrefs.SetInt("Try"+SubTipe.name, 3);
				PlayerPrefs.SetInt("TryTop", -1);
			}
			else if(ExtraPlus.name=="BouncinessExtra"){
				PlayerPrefs.SetInt("Try"+SubTipe.name, 4);
				PlayerPrefs.SetInt("TryTop", -1);
			}
			else if(ExtraPlus.name=="JumpExtra"){
				PlayerPrefs.SetInt("Try"+SubTipe.name, 5);
				PlayerPrefs.SetInt("TryTop", -1);
			}
			StartCarac.SetTry();
	 	}
		BallSkinStart.SetView();
	}

	public void ShowProp(){
		GameObject Nombre= GameObject.Find("Nombre1");
		Text NombreCarac = Nombre.GetComponent<Text>();
		NombreCarac.text=	Name.text;
		StartCarac.SetSlider(float.Parse(Masa.text),0);
		StartCarac.SetSlider(float.Parse(Vida.text),1);
		StartCarac.SetSlider(float.Parse(Resistencia.text),2);
		StartCarac.SetSlider(float.Parse(Arrastre.text),3);
		StartCarac.SetSlider(float.Parse(Rebote.text),4);
		StartCarac.SetSlider(Mathf.Round((float.Parse(Jump.text))/(float.Parse(Masa.text))),5);
	}

}
