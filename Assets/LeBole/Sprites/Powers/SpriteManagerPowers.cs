using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


enum SpriteTypePowers {Frozen1,Frozen2,Jump1,Jump2,Speed1,Speed2}


public class SpriteManagerPowers : MonoBehaviour {

	// Use this for initialization
	[SerializeField] 
	private SpriteTypePowers CurrentType;
	private SpriteTypePowers LastType;

	[SerializeField] 
	private SpriteAtlas Atlas;
	public Image ImageSprite;

	void Start () {
		ImageSprite = GetComponent<Image> ();
		LastType= SpriteTypePowers.Frozen1; 
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
