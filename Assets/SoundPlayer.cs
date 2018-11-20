using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    [SerializeField]AudioClip hit;
    [SerializeField] AudioClip collect;
    AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
    public void PlayHit() {
        source.clip = hit;
        source.Play();
    }
    public void PlayCollect() {
        source.clip = collect;
        source.Play();
    }
}
