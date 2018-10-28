using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private int speed = 50;
	private Civilian civilianScript;
	private Vector3 scale;
	private bool facingRight = false;
	private SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
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
				 sprite.flipX = true;
				 facingRight = true;
			 }
		}
		//Determine if player is moving left and flip sprite
		else if (x < 0)
		{
			 //Flip sprite if it is  not facing left already
			 if (facingRight)
			 {
				 sprite.flipX = false;
				 facingRight = false;
			 }
		}

		//Update player velocity
		//Remove Transform.Translate because it ignores physics
		GetComponent<Rigidbody>().velocity = new Vector3 (x*speed, 0, y*speed);
  }

	//Check collision of player and NPC
	void OnCollisionStay(Collision collision)
	{
		//If attack button is pressed
		if (Input.GetMouseButtonDown(0))
		{
			//If player is colliding with a civilian or the Android
			if (collision.collider.name == "Civilian" || collision.collider.name == "AI")
			{
				//Get the Civilian script and call the kill method
				civilianScript = collision.gameObject.GetComponent<Civilian>();
				civilianScript.kill();
				//Destroy(collision.gameObject);
			}
		}
	}
}
