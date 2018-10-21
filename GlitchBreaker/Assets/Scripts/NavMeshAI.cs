using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour {

	public NavMeshAgent agent;
	public Camera cam;
	public float timer = 5;
	private float timeCount;

	void Start() {
		agent.speed = 10f;

	}
	void Update() {
     // RaycastHit hit;
     // if (Input.GetMouseButtonDown(0)) {
     //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
     //    if (Physics.Raycast(ray, out hit))
     //       agent.SetDestination(hit.point);
		 //
     //    }
		 //transform.parent.position = transform.position;

		 timeCount += Time.deltaTime;
		 if(timeCount >= timer)
		 {
			 if (timeCount >= timer) {
				 Vector3 newPosition = RandomMove(transform.position, 25, -1);
				 agent.SetDestination(newPosition);
				 timeCount = 0;
			 }
		 }
  }

	static Vector3 RandomMove(Vector3 origin, float dist, int layermask) {
			Vector3 randDirection = Random.insideUnitSphere * dist;
			randDirection += origin;
			NavMeshHit navHit;
			NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
			return navHit.position;
		}
}
