using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBall{
public class BotonesMenu : MonoBehaviour {

	 public Animator menuAnimator;
	 public GameObject playerOBJ;
	 public GameObject Code;

	 //variable de control
	 private bool StopStoreAnimation;

	void Start () {
		menuAnimator=GetComponent<Animator>();
		playerOBJ.SetActive(false);
		menuAnimator.SetBool("B_ShowMenu", true);
		PlayerPrefs.SetInt("continue",0);
	}
	void Awake(){
		StopStoreAnimation=true;
	}

	void OnEnable	(){
		Debug.Log("continue? " + PlayerPrefs.GetInt("continue",0));
		if(PlayerPrefs.GetInt("continue",0)==1){
			ShowNiveles();
		}
	}

	void CodeSkins(){
		if(StopStoreAnimation){
		 Code.GetComponent<MenuDinamicoVertical>(). B_Skins();
		 StopStoreAnimation=false;
	 }
	}

	public void allFalseShow(){
		menuAnimator.SetBool("B_ShowMenu",false);
		menuAnimator.SetBool("B_ShowNiveles",false);
		menuAnimator.SetBool("B_ShowSkins",false);
		menuAnimator.SetBool("B_ShowOpciones", false);
		//menuAnimator.SetBool("B_ShowExtras", false);

		playerOBJ.SetActive(false);
	}
	// Update is called once per frame
	public void ShowMenu(){
		playerOBJ.SetActive(false);
		allFalseShow();
		menuAnimator.SetBool("B_ShowMenu", true);
	}
	public void ShowOpciones(){
		allFalseShow();
		menuAnimator.SetBool("B_ShowOpciones", true);
	}
	public void ShowNiveles(){
		allFalseShow();
		menuAnimator.SetBool("B_ShowNiveles", true);
	}
	public void ShowSkins(){
		allFalseShow();
		playerOBJ.SetActive(true);
		menuAnimator.SetBool("B_ShowSkins", true);

	}
	public void ShowExtras(){
		allFalseShow();
		playerOBJ.SetActive(true);
		menuAnimator.SetBool("B_ShowExtras", true);
	}

}
}
