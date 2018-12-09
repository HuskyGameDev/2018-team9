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
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public Sprite androidSprite;
    private int index;
    private bool androidSpriteActive = false;
    private float glitchTime = 1f; //seconds before reverting sprite back
    private float timeCount;
    private Sprite androidDisguiseSprite;
    private int glitchProbability = 1;
    private float gameTimeCount;
    private float glitchIncreaseTimer = 45f;
    public Sprite[] deadSprites;
    public int spriteIndex;
    private NavMeshAI nav;


	// Use this for initialization
	void Start () {

        eye = GameObject.FindGameObjectWithTag("Eye");
        civilian = this.gameObject;
        nav = civilian.GetComponent<NavMeshAI>();
        civTransform = civilian.transform;
        index = civTransform.GetSiblingIndex();

        //Don't run the Update() method if the NPC is an Android
 //       if (!isAndroid)
 //       {
          stealthScript = eye.GetComponent<Stealth>();
 //       }
	}

	// Update is called once per frame
  /*
   *  Stealth needs to work so that player can get close enough to kill them
   *
   */
	void Update ()
    {
      //Run this portion only for the android
      if (isAndroid)
      {
          gameTimeCount += Time.deltaTime;
          //Randomly set the android's sprite to be its metallic form
          if(Random.Range(0,1000) < glitchProbability && androidSpriteActive == false)
          {
            //Get spriteRenderer from child
            spriteRenderer = this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            //Hold on to the current sprite so it can be reset
            androidDisguiseSprite = spriteRenderer.sprite;
            //Update sprite to the android's true form
            spriteRenderer.sprite = androidSprite;
            androidSpriteActive = true;
          }
          //Wait for specified time and change sprite back to disguised form
          else if(androidSpriteActive)
          {
            timeCount += Time.deltaTime;
       		  if(timeCount >= glitchTime)
       		  {
              //Reset sprite
              spriteRenderer.sprite = androidDisguiseSprite;
              androidSpriteActive = false;
              //Short sprite changes between long sprite changes
              if (timeCount >= glitchTime+0.1)
              {
                timeCount = 0;
              }
            }

            //Increase probability of glitch occuring after set period of time
            if (gameTimeCount >= glitchIncreaseTimer)
            {
              glitchProbability += 1;
              gameTimeCount = 0;
            }
          }
      }
      //Run this portion only for civilians
      else
      {
          RaycastHit hit; //Hit detects if the player is in the NPCs point of view
            RaycastHit hitInCollider; //hitInCollider detects if the player is in proximity of the NPC

            //Get hit raycast for point of view
            float posHit = 5f;
            Vector3 vec = Vector3.right;
            float posColl = 1.5f;

            if (!nav.facingRight)
            {
                vec = Vector3.left;
                posHit = -5f;
                posColl = -1.5f;
            }
        
            if (Physics.Raycast(transform.position + new Vector3(posHit, 0.5f, 0), vec, out hit, Mathf.Infinity))
            {
                //Get hitInCollider raycast for proximity
                if (Physics.Raycast(transform.position + new Vector3(posColl, 0.5f, 0), vec, out hitInCollider, Mathf.Infinity))
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

        //Don't let the player be seen if he is hidden behind a wall
        if (collision.tag == "Inner Wall")
        {
            nextToWall = true;
            distanceFromWall = Vector3.Distance(collision.transform.position, civTransform.position);
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

    //Determine if NPC is colliding wiht player
    private void OnTriggerStay(Collider other)
    {
        if (nextToPlayer && other.name == "Player")
        {
            float distanceFromPlayer = Vector3.Distance(other.transform.position, civTransform.position);

            //player is not hidden behind wall
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
        Destroy(GetComponent<NavMeshAI>());
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>());

        Destroy(this.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
        this.gameObject.transform.GetChild(0).gameObject.transform.Rotate(0,0,90);

        //Change sprite when NPC dies
        if (isAndroid)
        {

          this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = androidSprite;
        }
        else
        {
          this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = deadSprites[spriteIndex];
        }

        Debug.Log(spriteIndex);
        //Disable calls to Update()
        enabled = false;

        Destroy(this.gameObject, 10.0f);
    }
}
