using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


enum SpriteTypeStart {Apuntador, Corazon, LevelFaces, AATacometroLevelSprite}


public class SpriteManagerStart : MonoBehaviour {

	// Use this for initialization
	[SerializeField] 
	private SpriteTypeStart CurrentType;
	private SpriteTypeStart LastType;

	[SerializeField] 
	private SpriteAtlas Atlas;
	public Image ImageSprite;

	void Start () {
		ImageSprite = GetComponent<Image> ();
		LastType= SpriteTypeStart.Apuntador; 
	}


	// Update is called once per frame
	void Update () {
		changeType ();
	}

	public void changeType (){
		if (LastType != CurrentType) {
			ImageSprite.sprite = Atlas.GetSprite (CurrentType.ToString ());
			LastType = CurrentType;
		}
	}
}
