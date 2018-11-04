using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour {

    public GameObject civ;
    GameObject civ2;

	// Use this for initialization
	void Start () {
        civ2 = Instantiate(civ, new Vector3(civ.transform.position.x + 10, civ.transform.position.y + 10, civ.transform.position.z + 10), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
