using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * 10, 0.8f), Mathf.Lerp(0, Input.GetAxis("Vertical") * 10, 0.8f));

        /*if (Input.GetKeyDown(KeyCode.A))
        {
            //left();
            //transform.Translate(-10 * Time.deltaTime, 0, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(-100*Time.deltaTime, 0, 0), Mathf.Lerp(0, 0, 0));
            Debug.Log("LEFT");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //down();
            Debug.Log("DOWN");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //right();
            Debug.Log("RIGHT");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //up();
            Debug.Log("UP");
        }*/

        var x = Input.GetAxisRaw("Horizontal") * 10f * Time.deltaTime;
        var y = Input.GetAxisRaw("Vertical") * 10f * Time.deltaTime;
        transform.Translate(x, y, 0);
    }
}
