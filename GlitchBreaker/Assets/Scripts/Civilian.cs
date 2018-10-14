using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {

    public GameObject eye;
    Stealth stealthScript;
    private bool isTriggered = false;

	// Use this for initialization
	void Start () {
        stealthScript = eye.GetComponent<Stealth>();
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(5f, 0, 0), Vector3.right, Mathf.Infinity);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "Player")
            {
                stealthScript.playerDetected = true;
            }
            else
            {
                if (!isTriggered)
                {
                    stealthScript.playerDetected = false;
                }
            }
            
        }


	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            isTriggered = true;
            stealthScript.playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;   
    }
}
