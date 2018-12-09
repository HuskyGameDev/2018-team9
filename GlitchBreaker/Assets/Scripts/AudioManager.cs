using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	
	public AudioClip myclip;

	// Use this for initialization
	void Start () {
		this.gameObject.AddComponent<AudioSource>();
        this.GetComponent<AudioSource>().clip = myclip;
	}
	
	public void Play() {
		this.GetComponent<AudioSource>().Play();
		DontDestroyOnLoad(this.gameObject);
	}
	
	public void PlayNew(AudioClip newClip) {
		this.gameObject.AddComponent<AudioSource>();
		this.GetComponent<AudioSource>().clip = newClip;
		this.GetComponent<AudioSource>().Play();
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
