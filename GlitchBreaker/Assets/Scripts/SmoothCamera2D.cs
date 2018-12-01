using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera2D : MonoBehaviour {

     public float dampTime = 0.15f;
     // Floats for checking if in bounds. -500/500 is just default
		 public float minPositionX = -500;
		 public float maxPositionX = 500;
		 // These are really Z changes. but for clarity in editing in script
		 // It is considered Y.
		 public float minPositionY = -500;
		 public float maxPositionY = 500;
     private Vector3 velocity = Vector3.zero;
     public Transform target;
 	 	 public Camera camera;

		 void Start()
		 {
			 // Let Script know specifically we are dealing with cam for WorldToViewportPoint
			 camera = GetComponent<Camera>();
		 }

     // Update is called once per frame
     void Update ()
     {
			 	 // If the player does something... may not need this. but i wouldn't.
         if (target)
         {
					 	 // point finds player
             Vector3 point = camera.WorldToViewportPoint(target.position);
						 // Setup distance between camera and player
             Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
						 // add up the two distances to get destination for SmoothDamp.
						 // See documentation of SmoothDamp.
             Vector3 destination = transform.position + delta;
						 // Attempt at keeping Camera from leaving map
						 if(destination.x < minPositionX)
						 {
							 destination.x = minPositionX;
						 }
						 else if(destination.x > maxPositionX)
						 {
							 destination.x = maxPositionX;
						 }
						 if(destination.z < minPositionY)
						 {
							 destination.z = minPositionY;
						 }
						 else if(destination.z > maxPositionY)
						 {
							 destination.z = maxPositionY;
						 }
						 // Camera follows at speed to player
             transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
         }

     }
 }
