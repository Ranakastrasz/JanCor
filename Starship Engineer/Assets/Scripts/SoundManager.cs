using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Active;
    public AudioSource explosion;
    public AudioSource beep;
    public AudioSource reflect;
    public AudioSource shoot;

    // Use this for initialization
    void Start ()
    {
        Active = this;
	}

    public void PlaySound(AudioSource sound, float pitch, float variance = 0)
    {
        sound.pitch = pitch + Random.Range(-variance, variance);
        sound.Play();
    }

}
