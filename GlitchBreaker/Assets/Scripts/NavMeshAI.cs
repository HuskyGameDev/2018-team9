using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour {

	public NavMeshAgent agent;
	public Camera cam;
	public float timer;
	private float timeCount;

	//initialization
	void Start() {
		agent.speed = 10f;

	}

	//Update method called every frame
	//Waits specified amount of time before calculating new position
	void Update() {

		 //Wait specified amount of time before finding new point to move
		 timeCount += Time.deltaTime;
		 if(timeCount >= timer)
		 {
			 	 //Find new point to move to
				 Vector3 newPosition = RandomMove(transform.position, -1);
				 //Start moving the Android by setting the new position
				 agent.SetDestination(newPosition);
				 timeCount = 0;
		 }
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
