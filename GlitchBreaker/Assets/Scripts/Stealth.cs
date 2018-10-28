using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour {

    public bool playerDetected = false;
    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerDetected)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.green;
        }
	}
}
