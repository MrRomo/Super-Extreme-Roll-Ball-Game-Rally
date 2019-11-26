using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


enum SpriteTypeHUD {ButtonAcceleratorOverSprite,ButtonAcceleratorUpSprite,ButtonArrowOverSprite,ButtonArrowUpSprite,
	ButtonBrakeOverSprite, ButtonBrakeUpSprite,ButtonCameraCycleUpSprite,ButtonResetSprite,ButtonSpacebarSprite,
	ButtonThumbstickOverSprite,	ButtonThumbstickUpSprite,ButtonTimescaleFullUpSprite,ButtonTimescaleSlowUpSprite,
	SliderBackgroundSprite, SliderHandleSprite,Tacometro,TacometroFill,TacometroFillPlus,TouchpadSprite,Pausa}


public class SpriteManagerHUD : MonoBehaviour {

	// Use this for initialization
	[SerializeField] 
	private SpriteTypeHUD CurrentType;
	private SpriteTypeHUD LastType;

	[SerializeField] 
	private SpriteAtlas Atlas;
	public Image ImageSprite;

	void Start () {
		ImageSprite = GetComponent<Image> ();
		LastType= SpriteTypeHUD.ButtonAcceleratorOverSprite; 
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
