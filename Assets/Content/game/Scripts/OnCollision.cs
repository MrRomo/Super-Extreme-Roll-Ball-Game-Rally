using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSettings))]
public class OnCollision : MonoBehaviour
{
    public AudioClip clip;
    new AudioSource audio;
    public Collider triggeredBy;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.timeSinceLevelLoad < 0.1) return;
        if (clip == null) return;
        if (triggeredBy != null && collision.collider != triggeredBy) return;
        audio.PlayOneShot(clip);
    }
}
