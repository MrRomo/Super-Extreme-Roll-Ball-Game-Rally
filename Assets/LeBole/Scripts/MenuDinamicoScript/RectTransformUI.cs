using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformUI : MonoBehaviour {


	public RectTransform ScrollPanel;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		 if(Input.GetKeyDown("1")){
			 ScrollPanel.offsetMin= new Vector2(0,0);
			 ScrollPanel.offsetMax= new Vector2(20,-350);
		 }
		 if(Input.GetKeyDown("2")){
			ScrollPanel.offsetMin= new Vector2(1,-500);
			ScrollPanel.offsetMax= new Vector2(0,0);
		}
		if(Input.GetKeyDown("3")){
			ScrollPanel.offsetMin= new Vector2(0,0);
			ScrollPanel.offsetMax= new Vector2(500,1);
		}
		if(Input.GetKeyDown("4")){
		 ScrollPanel.offsetMin= new Vector2(-500,1);
		 ScrollPanel.offsetMax= new Vector2(0,0);
	 }

	}
}
