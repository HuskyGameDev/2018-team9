using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {

    public GameObject eye;
    Stealth stealthScript;
    private bool isTriggered = false;
    private float timeInTrigger = 0.0f;

	// Use this for initialization
	void Start () {
        stealthScript = eye.GetComponent<Stealth>();
	}
	
	// Update is called once per frame

    /*
     *  Stealth needs to work so that player can get close enough to kill them 
     * 
     */
	void Update () {

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(5f, 0, 0), Vector3.right, Mathf.Infinity);
        RaycastHit2D hitInCollider = Physics2D.Raycast(transform.position + new Vector3(1.5f, 0, 0), Vector3.right, Mathf.Infinity);

//        print(hit.collider.gameObject);

        if (hit.collider != null || hitInCollider.collider != null)
        {
            if (hit.collider.gameObject.name == "Player" || hitInCollider.collider.gameObject.name == "Player")
          {
                stealthScript.playerDetected = true;
            }
            else
            {
                DetectPlayer();
            }
        }
        else
        {
            DetectPlayer();            
        }


    }

    private void DetectPlayer()
    {
        if (!isTriggered)
        {
            stealthScript.playerDetected = false;
        }
        else
        {
            timeInTrigger += Time.deltaTime;
 //           print(timeInTrigger);

            if (timeInTrigger >= 1f)
            {
                stealthScript.playerDetected = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
        timeInTrigger = 0.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        stealthScript.playerDetected = true;
    }
}