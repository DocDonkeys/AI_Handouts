using UnityEngine;
using System.Collections;

public class SteeringWander : MonoBehaviour {

	public float min_distance = 0.1f;
	public float time_to_target = 0.25f;
    public float wander_radius = 5.0f;

    private Vector3 randTarget;

    Move move;
    SteeringSeek seek;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();
        randTarget = move.target.transform.position;
    }

	// Update is called once per frame
	void Update () 
	{
        // TODO Homework: Update the target location to a random point in a circle
        // You could just call seek.Steer() / arrive.Steer() or simply do the calculations by yourself
        // like the code below.
        Vector3 diff = randTarget - transform.position;

        if (diff.magnitude < min_distance)
        {
            float prev_y = randTarget.y;
            randTarget = move.target.transform.position + Random.insideUnitSphere * wander_radius;
            randTarget.y = prev_y;    //We don't want to move vertically
        }

        seek.Steer(randTarget);
	}

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);
	}
}
