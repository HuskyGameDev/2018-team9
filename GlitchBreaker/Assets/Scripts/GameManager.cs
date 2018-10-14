using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //----------------------------------------
    // handles
    public SceneTransition UI;

    //-----------------------------------------
    // function definitions
	void Start() 
	{
		TogglePauseMenu();
	}

    public void TogglePauseMenu()
    {
		//Toggle timescale on and remove pause menu
        if (UI.GetComponentInChildren<Canvas>().enabled)
        {
            UI.GetComponentInChildren<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
        }
		//Toggle timescale off and show pause menu
        else
        {
            UI.GetComponentInChildren<Canvas>().enabled = true;
            Time.timeScale = 0f;
        }
        
        Debug.Log("GAMEMANAGER:: TimeScale: " + Time.timeScale);
    }

}