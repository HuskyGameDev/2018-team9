using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour {

	public NavMeshAgent agent;
	public Camera cam;
	public float timer;
	private float timeCount;
	private Animator anim;
    private Vector3 newPosition;
    private Vector3 previousPosition; //Position in the previous frame
    private bool facingRight = false;
    private float facingTimer = 0;

    //initialization
    void Start() {
		agent.speed = 10f;
		anim = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        previousPosition = new Vector3(0,0,0);
	}

	//Update method called every frame
	//Waits specified amount of time before calculating new position
	void Update() {
         
		 //Wait specified amount of time before finding new point to move
		 timeCount += Time.deltaTime;
         /*if (this.GetComponent<Rigidbody>().velocity.magnitude <= 500 && timeCount > 1)
         {
            //Debug.Log("HERE");
            anim.SetBool("Walking", false);
         }
         else { anim.SetBool("Walking", true); }*/

         if (timeCount >= timer)
		 {
			 	 //Find new point to move to
				 newPosition = RandomMove(transform.position, -1);
				 //Start moving the Android by setting the new position
				 agent.SetDestination(newPosition);
				 //anim.SetBool("Walking", true);
				 timeCount = 0;
		 }

         //Flip sprite based on walking direction
         //Walking right
         facingTimer += Time.deltaTime;
		 if (this.transform.position.x > previousPosition.x)
         {
            if (!facingRight && facingTimer >=1)
            {
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = true;
                facingRight = true;
                facingTimer = 0;
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().center = new Vector3(-1, 0);
            }
        }
         //Walking left
         else if (this.transform.position.x < previousPosition.x)
         {
            if (facingRight && facingTimer >= 1)
            {
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = false;
                facingRight = false;
                facingTimer = 0;
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().center = new Vector3(1, 0);
            }
         }


        previousPosition = this.transform.position;
    }

	/*This method randomizes a point on the map to move the Android
	  and returns the position to move to*/
	static Vector3 RandomMove(Vector3 origin, int layermask) {
			Vector3 randPoint = new Vector3(0f,0f,0f);

			//Randomize a point on the map
			randPoint.x = Random.Range(-57f, 57f);
			randPoint.z = Random.Range(-30f, 30f);
			NavMeshHit navHit;

			//Find point on navmesh closest to the randomized point
			NavMesh.SamplePosition(randPoint, out navHit, 30, layermask);
			return navHit.position;
		}
}
