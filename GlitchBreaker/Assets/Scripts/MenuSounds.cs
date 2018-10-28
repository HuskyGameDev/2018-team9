using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
	private static AudioSource menuMusic;
	// Use this for initialization
	void Start ()
	{
		 //Get all menu sound objects
		 var sounds = GameObject.FindGameObjectsWithTag ("PersistentMenuSound");

		 //Loop for each menu sound object and assign the AudioSource component
		 foreach (var sound in sounds)
		 {
			 	//Other sounds added later
			  //Background menu music song
			 	if (sound.name == "MenuMusic")
		    	 menuMusic = sound.GetComponent<AudioSource>();
		 }

		 //Play song if it isn't already playing
		 var index = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
 	 	 if (menuMusic != null && !menuMusic.isPlaying && index < 5)
				 menuMusic.Play();
	}

}
