using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    GameObject civilians;
    NPCspawn npc;
    private int numCivilians = 0;
    private bool[] detection;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        civilians = GameObject.FindGameObjectWithTag("Civilians");
        npc = civilians.GetComponent<NPCspawn>();
        numCivilians = npc.numCivilians;
        detection = new bool[numCivilians];
    }

    public void DetectPlayer(int index, bool detected)
    {
        detection[index] = detected;
    }

    // Update is called once per frame
    void Update()
    {
        bool playerDetected = false;

        for (int i = 0; i < numCivilians; i++)
        {
            if (detection[i])
            {
                playerDetected = true;
            }
        }

        //Update the eye color if the player is detected
        if (playerDetected)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.green;
        }
    }
}
