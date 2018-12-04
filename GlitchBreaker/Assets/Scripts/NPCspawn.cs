using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCspawn : MonoBehaviour {

	public GameObject background;
	private Transform mapBoundary;
	public GameObject civilianPrefab;
	private GameObject civilianInstance;
	public GameObject eye;
	public Sprite[] sprites;
	private Collider[] spawnPositions;

	public int numCivilians;

	// Use this for initialization
	void Start () {
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

    //This method spawns civilians in random locations and makes sure they don't overlap with each other
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

		//Instantiate game object and set components that can't be assigned in prefab
		civilianInstance = Instantiate(civilianPrefab, position, rotation);
        civilianInstance.GetComponent<Civilian>().spriteIndex = i;
		civilianInstance.transform.SetParent(GameObject.Find("Civilians").transform);
		civilianInstance.GetComponent<Civilian>().eye = eye;
		civilianInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[i%sprites.Length];

		//Make sure it doesn't overlap previously spawned civilians
		for (int j = 0; j < i; j++)
		{
			//Check if collider overlaps another civilian collider
			if (civilianInstance.transform.GetChild(0).GetComponent<Collider>().bounds.Intersects(spawnPositions[j].bounds))
			{
				//Randomize new location
				position.x = Random.Range(-(mapBoundary.localScale.x/2f-6f), (mapBoundary.localScale.x/2f-6f));
				position.z = Random.Range(-(mapBoundary.localScale.z/2f-6f), (mapBoundary.localScale.z/2f-6f));
				civilianInstance.transform.position = position;
				j = 0; //Start loop over
			}
		}

		//Add this collider to array
		spawnPositions[i] = civilianInstance.transform.GetChild(0).GetComponent<Collider>();
	}
}
