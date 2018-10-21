using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private int speed = 50;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	//Get input from keyboard
        var x = Input.GetAxisRaw("Horizontal") * 50f * Time.deltaTime;
        var y = Input.GetAxisRaw("Vertical") * 50f * Time.deltaTime;

	//Update player velocity
	//Remove Transform.Translate because it ignores physics
	GetComponent<Rigidbody>().velocity = new Vector3 (x*speed, 0, y*speed);

    }
}
