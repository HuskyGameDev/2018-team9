using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private int speed = 75;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	//Get input from keyboard
        var x = Input.GetAxisRaw("Horizontal") * 10f * Time.deltaTime;
        var y = Input.GetAxisRaw("Vertical") * 10f * Time.deltaTime;

	//Update player velocity
	//Remove Transform.Translate because it ignores physics
	GetComponent<Rigidbody2D>().velocity = new Vector2 (x*speed, y*speed);

    }
}
