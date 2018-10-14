using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, Mathf.Infinity);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "Player")
            {
                print(hit.collider.gameObject.name);
            }
            
        }


	}
}
