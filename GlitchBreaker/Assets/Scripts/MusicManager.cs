using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {
     public AudioClip menu;
	 public AudioSource music;

	// Use this for initialization
	void Start () {
		music = this.gameObject.AddComponent<AudioSource>();
        music.clip = menu;
		music.Play();
		music.loop = true;
		DontDestroyOnLoad(music);
	}
	
	// End any song based on environment
	public void EndSong() {
		music.Stop();
	}
	
	// Start a new song based on level/menu
	public void PlayNew(AudioClip newClip) {
		music.clip = newClip;
		music.Play();
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
