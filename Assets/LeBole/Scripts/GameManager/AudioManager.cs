using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameBall
{



    public class AudioManager : MonoBehaviour
    {

        // Use this for initialization
        public AudioSource Jump_Audio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
        public AudioSource Hurt_Audio;
        public AudioSource PickUP_Audio;
        public AudioSource Toch_Audio;
        public AudioSource PwUP_Audio;
		    public AudioSource WorldSource;


               // Audio to play when the ball jump.
        public AudioClip JumpingAudio;            // Audio to play when the ball jump.
        public AudioClip HurtingAudio;
        public AudioClip PickUpAudio;

        public AudioClip TochAudio;
        public AudioClip PwUPAudio;
    		public AudioClip Music;
    		public AudioClip WinSound;


        public float m_PitchRange = 0.2f;

        private float m_OriginalPitch;
        private HealtBall HealtBall;
        void Start()
        {
            m_OriginalPitch = Jump_Audio.pitch;
            HealtBall = GetComponent<HealtBall>();
			      WorldSource.clip = Music;
			      WorldSource.Play();
        }

        // Update is called once per frame
        void Update()
        {

        }

    		public void WinSeq(){
    			WorldSource.clip = WinSound;
    			WorldSource.loop = false;
    			WorldSource.Play();
    		}

        public void EngineAudioHurt()
        {
            if (HealtBall.m_Dead == false)
            {
                Hurt_Audio.clip = HurtingAudio;
                Hurt_Audio.Play();
            }

        }



        public void EngineAudioJump()
        {
            Jump_Audio.clip = JumpingAudio;
            Jump_Audio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
            Jump_Audio.Play();

        }
        public void EngineAudioToch()
        {
            Toch_Audio.clip = TochAudio;
            Toch_Audio.Play();

        }
        public void EngineAudioPickUp()
        {
            PickUP_Audio.clip = PickUpAudio;
            PickUP_Audio.pitch = m_OriginalPitch + 0.5f;
            if (PickUP_Audio.pitch > 2)
            {
                PickUP_Audio.pitch = 1;
            }
            PickUP_Audio.Play();
      }
      public void EngineAudioPwUp()
      {
          PwUP_Audio.clip = PwUPAudio;
          PwUP_Audio.Play();
      }

    }
}
