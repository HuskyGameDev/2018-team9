using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {

    public GameObject eye;
    Stealth stealthScript;

	// Use this for initialization
	void Start () {
        stealthScript = eye.GetComponent<Stealth>();
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(1.5f, 0, 0), Vector3.right, Mathf.Infinity);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "Player")
            {
                stealthScript.playerDetected = true;
            }
            else
            {
                stealthScript.playerDetected = false;
            }
            
        }


	}
}
