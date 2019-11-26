﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using GameBall;
using UnityEngine.UI;

public class PostProcesingControl : MonoBehaviour {



	public GameObject player;
	public Toggle ChromaB, BloomB, ColorB;
	PostProcessingBehaviour filters;
	PostProcessingProfile profile;
	ChromaticAberrationModel.Settings Chroma;
	MotionBlurModel.Settings MotionB;
	BloomModel.Settings BloomM;
	ColorGradingModel.Settings ColorG;

	// Use this for initialization
	void Start () {
		PostProcessingBehaviour filters = GetComponent<PostProcessingBehaviour>();
		PostProcessingProfile profile = filters.profile;
		profile.chromaticAberration.enabled= ChromaB.isOn; 
		profile.bloom.enabled= BloomB.isOn; 
		profile.colorGrading.enabled= ColorB.isOn; 
		profile.motionBlur.enabled= ChromaB.isOn; 

	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<BallPower>().SPSc>0){
			PostProcessingBehaviour filters = GetComponent<PostProcessingBehaviour>();
			PostProcessingProfile profile = filters.profile;
			Chroma = profile.chromaticAberration.settings;
			MotionB= profile.motionBlur.settings;
			Chroma.intensity = 1;	
			MotionB.frameBlending = 0.2f;
			MotionB.shutterAngle= 270;

			profile.chromaticAberration.settings = Chroma; 
			profile.motionBlur.settings = MotionB;

		}
		else{
			PostProcessingBehaviour filters = GetComponent<PostProcessingBehaviour>();
			PostProcessingProfile profile = filters.profile;
			Chroma = profile.chromaticAberration.settings;
			MotionB= profile.motionBlur.settings;
			Chroma.intensity = 0;	
			MotionB.frameBlending = 0;
			MotionB.shutterAngle= 0;

			profile.chromaticAberration.settings = Chroma; 
			profile.motionBlur.settings = MotionB; 

		}
	}

	public void setChroma(){
		PostProcessingBehaviour filters = GetComponent<PostProcessingBehaviour>();
		PostProcessingProfile profile = filters.profile;
		profile.chromaticAberration.enabled= ChromaB.isOn; 
		profile.motionBlur.enabled= ChromaB.isOn; 

	}

	public void setBloom(){
		PostProcessingBehaviour filters = GetComponent<PostProcessingBehaviour>();
		PostProcessingProfile profile = filters.profile;
		profile.bloom.enabled= BloomB.isOn; 
	}
	public void setColorG(){
		PostProcessingBehaviour filters = GetComponent<PostProcessingBehaviour>();
		PostProcessingProfile profile = filters.profile;
		profile.colorGrading.enabled= ColorB.isOn; 
	}
}
