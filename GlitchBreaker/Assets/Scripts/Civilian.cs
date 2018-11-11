using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {

    public GameObject eye;
    Stealth stealthScript;
    private bool isTriggered = false;
    private float timeInTrigger = 0.0f;
    public bool isAndroid = false;

    private bool nextToWall = false;
    private bool nextToPlayer = true;
    private float distanceFromWall;
    public GameObject civilian;
    Transform civTransform;
    int index;

	// Use this for initialization
	void Start () {

        eye = GameObject.FindGameObjectWithTag("Eye");
        civilian = this.gameObject;
        civTransform = civilian.transform;
        index = civTransform.GetSiblingIndex();

        //Don't run the Update() method if the NPC is an Android
        if (isAndroid)
        {
          enabled = false;
        }
        else
        {
          stealthScript = eye.GetComponent<Stealth>();
        }
	}

	// Update is called once per frame

    /*
     *  Stealth needs to work so that player can get close enough to kill them
     *
     */
	void Update () {

        RaycastHit hit; //Hit detects if the player is in the NPCs point of view
        RaycastHit hitInCollider; //hitInCollider detects if the player is in proximity of the NPC

        //Get hit raycast for point of view
        if (Physics.Raycast(transform.position + new Vector3(5f, 0, 0), Vector3.right, out hit, Mathf.Infinity))
        {
            //Get hitInCollider raycast for proximity
            if (Physics.Raycast(transform.position + new Vector3(1.5f, 0, 0), Vector3.right, out hitInCollider, Mathf.Infinity))
            {
                if (CheckRayCast(hit, 5f))
                {
                    stealthScript.DetectPlayer(index, true);
                }
                else if (CheckRayCast(hitInCollider, 1.5f))
                {
                    stealthScript.DetectPlayer(index, true);
                }
                else
                {
                    DetectPlayer();
                }
            }
        }
    }

    private bool CheckRayCast(RaycastHit hit, float distance)
    {
        if (hit.collider != null) {
            if (hit.collider.gameObject.name == "Player")
            {
                return true;
            }
        }
        return false;
    }

    //Check if the player is in the sphere collider
    private void DetectPlayer()
    {
        //If the player is out of the sphere collider and isn't colliding with the NPC
        if (!isTriggered)
        {
            //Set the eye to green
            stealthScript.DetectPlayer(index, false);
        }
        //If the player is in the sphere collider for 1 second, set the eye to red
        else
        {
            //Wait 1 second
            timeInTrigger += Time.deltaTime;
            if (timeInTrigger >= 1f)
            {
                stealthScript.DetectPlayer(index, true);
            }
        }
    }


    //If the player is in the sphere collider, set stealth to triggered
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Inner Wall")
        {
            nextToWall = true;
            distanceFromWall = Vector3.Distance(collision.transform.position, civTransform.position);
//            print("Distance From Wall: " + distanceFromWall);
        }
        //If the colliding object is the player
        if (collision.name == "Player")
        {
            if (!nextToWall)
            {
                isTriggered = true;
            }
            else
            {
                nextToPlayer = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (nextToPlayer && other.name == "Player")
        {
//            print(Vector3.Distance(other.transform.position, civTransform.position));
            float distanceFromPlayer = Vector3.Distance(other.transform.position, civTransform.position);
//            print("Distance From Player: " + distanceFromPlayer);
            if (distanceFromPlayer < distanceFromWall)
            {
                isTriggered = true;
            }
        }
    }


    //Set stealth to untriggered once the player leaves the sphere collider
    private void OnTriggerExit(Collider collision)
    {
        isTriggered = false;
        timeInTrigger = 0.0f;
    }

   
    //If the player collides with the civilian, set the player detected state to true
    private void OnCollisionEnter(Collision collision)
    {
        //Civilian is not an Android
        
        if (!isAndroid)
        {
          stealthScript.DetectPlayer(index, true);
        }
        
    }
    

    //Kill civilian or Android
    public void kill()
    {
      //Delete colliders and set rotation
      //Android and civilian have different hierarchy on scene
      if (isAndroid)
      {
        Destroy(GetComponent<NavMeshAI>());
        Destroy(this.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
        this.gameObject.transform.GetChild(0).gameObject.transform.Rotate(0,0,90);
      }
      else
      {
        Destroy(GetComponent<BoxCollider>());
        transform.Rotate(0,0,90);
      }
      //Disable calls to Update()
      enabled = false;
    }
}
