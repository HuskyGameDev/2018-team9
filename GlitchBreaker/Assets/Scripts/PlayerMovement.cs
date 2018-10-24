using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private int speed = 50;
	private Vector3 scale;
	private bool facingRight = false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		//Get input from keyboard
  	var x = Input.GetAxisRaw("Horizontal") * 50f * Time.deltaTime;
    var y = Input.GetAxisRaw("Vertical") * 50f * Time.deltaTime;

		//Determine if player is moving right and flip sprite
		if (x > 0)
		{
			 //Flip sprite if it is not facing right already
			 if (!facingRight)
			 {
				 scale = transform.localScale;
			   scale.x = -1 * scale.x;
			   transform.localScale = scale;
				 facingRight = true;
			 }
		}
		//Determine if player is moving left and flip sprite
		else if (x < 0)
		{
			 //Flip sprite if it is  not facing left already
			 if (facingRight)
			 {
				 scale = transform.localScale;
			   scale.x = -1 * scale.x;
			   transform.localScale = scale;
				 facingRight = false;
			 }
		}

		//Update player velocity
		//Remove Transform.Translate because it ignores physics
		GetComponent<Rigidbody>().velocity = new Vector3 (x*speed, 0, y*speed);

  }
}
