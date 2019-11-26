
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InstanciarResultado : MonoBehaviour {
	//InNstancias
	public RectTransform PanelScroll;
	public GameObject PanelScroll_parent;
	public GameObject ResultadoX;

	//UI
	public Text CuadroInputResultados;
	public Text CuadroOutputResultados;
	public Image ProgressBar;

	//Variables de control y calculo
	private float Scroll=100f;
	private int nResultados;
	private bool bIntanciando;

	void Start () {
		CuadroOutputResultados.text="";
		ProgressBar.fillAmount=0f;
		bIntanciando=false;
	}
	public void B_Reload(){
		if(!bIntanciando){
			StartCoroutine(DestruirResultados());
			PanelScroll.offsetMin= new Vector2(8f, 310);
			}


	}
	IEnumerator DestruirResultados(){
		if((nResultados!=0)){
			for (int i =0; i<=nResultados; i++){
				Debug.Log("DESTRUYENDO EL " + i);
					GameObject ResultadoEncontrado= GameObject.Find("Resultado0"+(nResultados-i));
					if(ResultadoEncontrado!=null){
						Destroy(ResultadoEncontrado);
						CuadroOutputResultados.text=(nResultados-i).ToString();
						Porcentaje(nResultados,nResultados-i);
					}
					else if(ResultadoEncontrado==null){
						CuadroOutputResultados.text="";
						Porcentaje(0,nResultados-i);

					}
						yield return 0;
			}
			PanelScroll.offsetMin= new Vector2(8f, 310);
			nResultados=0;

		}
	}
	public void ScrollDownDim(float pScroll){
		PanelScroll.offsetMin= new Vector2(8f, 310);
		//PanelScroll.offsetMax= new Vector2(-8f, 15f);
		PanelScroll.offsetMin= new Vector2(8f, PanelScroll.offsetMin.y-pScroll);
		//PanelScroll.offsetMax= new Vector2(PanelScroll.offsetMax.x, PanelScroll.offsetMax.y+pScroll);
	}

	IEnumerator ResultInsntant(string ResName, float pResOffMin, float pResOffMax){
		GameObject ResultadoHijo = Instantiate(ResultadoX) as GameObject;
		ResultadoHijo.transform.SetParent(PanelScroll.transform);
		ResultadoHijo.name= "Resultado" + ResName;
		RectTransform ResultadoPanel = ResultadoHijo.GetComponent<RectTransform>();

		ResultadoPanel.localScale								= new Vector3(1f,1f,1f);
		ResultadoPanel.anchorMin								= new Vector2(0f,0f);
		ResultadoPanel.anchorMax								= new Vector2(1f 	,1f);
		ResultadoPanel.pivot										= new Vector2(0.5f,0.5f);
		ResultadoPanel.anchoredPosition3D				= new Vector3(0f,0f,0f);

		ResultadoPanel.offsetMin= new Vector2(8, pResOffMin);
		ResultadoPanel.offsetMax= new Vector2(-8, -pResOffMax);
		yield return 0;
	}

	IEnumerator SetInstantResultados(int num){


		int num2=num;
		nResultados=num-1;
		if(!bIntanciando){
				ScrollDownDim(Scroll*(num-1));
			  bIntanciando=true;
		for (int i=0; i<num; i++){
				num2-=1;
				CuadroOutputResultados.text=(i+1).ToString();
				string name= "0"+i.ToString();
				StartCoroutine(ResultInsntant(name,Scroll*num2+10, Scroll*i+10));
				Porcentaje(num,i+1);
				Debug.Log("Repe"+i);
				yield return 0;
			}
		}
		bIntanciando=false;
	}
	public void BotonInstantiator(){
		if(!bIntanciando){
			for (int i =0; i<=nResultados; i++){
				Debug.Log("DESTRUYENDO EL " + i);
					GameObject ResultadoEncontrado= GameObject.Find("Resultado0"+(nResultados-i));
					if(ResultadoEncontrado!=null){
						Destroy(ResultadoEncontrado);
						CuadroOutputResultados.text=(nResultados-i).ToString();
						Porcentaje(nResultados,nResultados-i);
					}
					else if(ResultadoEncontrado==null){
						CuadroOutputResultados.text="";
						Porcentaje(0,nResultados-i);

					}
				}


		int nums;
		if (int.TryParse(CuadroInputResultados.text, out nums)){
			if(nums>0){
				nums = int.Parse(CuadroInputResultados.text);
				StartCoroutine(SetInstantResultados(nums));
			}
	 		}
 		}
	}

	public void Porcentaje (int total, int actual){

		float porcentajea= ((100f*actual)/total)/100f;
		//Debug.Log("fillAmount "+porcentajea);
		ProgressBar.fillAmount= porcentajea;

	}
}
