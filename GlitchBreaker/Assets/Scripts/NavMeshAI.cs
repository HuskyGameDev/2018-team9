using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour {

	public NavMeshAgent agent;
	public Camera cam;

	void Update() {
     RaycastHit hit;
     if (Input.GetMouseButtonDown(0)) {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
           agent.SetDestination(hit.point);

        }
    }
}
