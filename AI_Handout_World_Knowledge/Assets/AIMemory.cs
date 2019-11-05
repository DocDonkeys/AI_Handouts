﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// TODO 1: Create a simple class to contain one entry in the blackboard
// should at least contain the gameobject, position, timestamp and a bool
// to know if it is in the past memory
public class blackboard_item
{
    public blackboard_item(GameObject obj_, Vector3 position_, float timestamp_)
    {
        obj = obj_;
        position = position_;
        timestamp = timestamp_;
        in_memory = false;
    }

    public GameObject obj;
    public Vector3 position;
    public float timestamp;
    public bool in_memory;
}

public class AIMemory : MonoBehaviour {

	public GameObject Cube;
	public Text Output;
    public Dictionary<string, blackboard_item> memory;

    // Use this for initialization
    void Start ()
    {
        // TODO 2: Declare and allocate a dictionary with a string as a key and
        // your previous class as value
        memory = new Dictionary<string, blackboard_item>();
    }

// Update is called once per frame
void Update () 
	{
        // TODO 3: Capture perception events and add an entry if the player is detected
        // if the player stop from being seen, the entry should be "in the past memory"


        // TODO 4: Add text output to the bottom-left panel with the information
        // of the elements in the Knowledge base

        Output.text = "< -- database display -- >";
	}

}
