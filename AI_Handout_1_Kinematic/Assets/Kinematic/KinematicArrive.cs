﻿using UnityEngine;
using System.Collections;

public class KinematicArrive : MonoBehaviour {

	public float min_distance = 0.1f;
	public float time_to_target = 0.25f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
        // TODO 8: calculate the distance. If we are in min_distance radius, we stop moving
        // Otherwise devide the result by time_to_target (0.25 feels good)
        // Then call move.SetMovementVelocity()
        Vector3 dist = move.target.transform.position - transform.position;
        if (dist.magnitude < min_distance) {
            move.SetMovementVelocity(Vector3.zero);
        }
        else {
            dist.x /= time_to_target;
            dist.y /= time_to_target;
            dist.z /= time_to_target;

            move.SetMovementVelocity(dist);
        }
	}

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);
	}
}
