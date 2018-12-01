using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCspawn : MonoBehaviour {

	public GameObject background;
	private Transform mapBoundary;
	public GameObject civilianPrefab;
	public GameObject androidPrefab;
	private GameObject civilianInstance;
	public GameObject eye;
	public Sprite[] sprites;
	private Collider[] spawnPositions;
	public int numCivilians;
	private int android;


	// Use this for initialization
	void Start () {
		android = Random.Range(0, numCivilians - 1);
		mapBoundary = background.GetComponent<Transform>();   //Map boundary
		spawnPositions = new Collider[numCivilians];				//Array of Colliders of previously spawned civilians

		//Spawn civilians
		for (int i = 0; i < numCivilians; i++)
		{
			spawnCivilians(i);
		}

	}

	// Update is called once per frame
	void Update () {
	}

	void spawnCivilians(int i)
	{
		Vector3 position;			//Position to spawn game object
		Quaternion rotation;  //Rotation of game object

		//Random location within map boundary
		position = new Vector3(0, 0.5f, 0);
		position.x = Random.Range(-(mapBoundary.localScale.x/2f-6f), (mapBoundary.localScale.x/2f-6f));
		position.z = Random.Range(-(mapBoundary.localScale.z/2f-6f), (mapBoundary.localScale.z/2f-6f));

		//Set rotation
		rotation = Quaternion.Euler(90,0,0);

		if (i != android) {
			//Instantiate game object and set components that can't be assigned in prefab
			civilianInstance = Instantiate (civilianPrefab, position, rotation);
			civilianInstance.transform.SetParent (GameObject.Find ("Civilians").transform);
			civilianInstance.GetComponent<Civilian> ().eye = eye;
			civilianInstance.GetComponent<Civilian> ().isAndroid = false;
			civilianInstance.GetComponent<SpriteRenderer> ().sprite = sprites [i % sprites.Length];
		}
		else {
			civilianInstance = Instantiate (androidPrefab, position, rotation);
			civilianInstance.GetComponent<Civilian> ().eye = eye;
			Transform androidSprite = civilianInstance.transform.GetChild (0);
			androidSprite.gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [i % sprites.Length];
//			civilianInstance.GetComponent<SpriteRenderer> ().sprite = sprites [i % sprites.Length];
			civilianInstance.GetComponent<Civilian> ().isAndroid = true;
		}

		//Make sure it doesn't overlap previously spawned civilians
		for (int j = 0; j < i; j++)
		{
			//Check if collider overlaps another civilian collider
			if (civilianInstance.GetComponent<Collider>().bounds.Intersects(spawnPositions[j].bounds))
			{
				//Randomize new location
				position.x = Random.Range(-(mapBoundary.localScale.x/2f-6f), (mapBoundary.localScale.x/2f-6f));
				position.z = Random.Range(-(mapBoundary.localScale.z/2f-6f), (mapBoundary.localScale.z/2f-6f));
				civilianInstance.transform.position = position;
				j = 0; //Start loop over
			}
		}

		//Add this collider to array
		spawnPositions[i] = civilianInstance.GetComponent<Collider>();
	}
}
