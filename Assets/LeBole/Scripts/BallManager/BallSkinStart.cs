using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkinStart : MonoBehaviour {

	GameObject playerObject;

	public Material[] BallSkin;
	public MeshRenderer BallSkinPlayer;
	public GameObject[] ExtraC_OBJ, ExtraP_OBJ;
	public MeshFilter MeshBall;
	public Mesh[] MeshBallPersonaje;
	int a,b,c;
	public void Awake(){
		SetView ();
	}

	public void SetView (){
		for(int i=0; i<6; i++){
				ExtraP_OBJ[i].SetActive(false);
				ExtraC_OBJ[i].SetActive(false);
		}
		ExtraC_OBJ[PlayerPrefs.GetInt ("SelecExtraCabezaView", 0)].SetActive(true);
		ExtraP_OBJ[PlayerPrefs.GetInt ("SelecExtraPiesView", 0)].SetActive(true);
		BallSkinPlayer.material = BallSkin [PlayerPrefs.GetInt ("SelecSkinView", 0)];
		MeshBall.mesh=MeshBallPersonaje[PlayerPrefs.GetInt("BallMeshTry", 0)];
		Debug.Log("MeshSelec: " +PlayerPrefs.GetInt("BallMesh", 0));
	}

	void Update ()
	{

	}

}
