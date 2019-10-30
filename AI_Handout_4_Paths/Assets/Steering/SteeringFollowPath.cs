using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class SteeringFollowPath : MonoBehaviour {

	Move move;
	SteeringSeek seek;

    public BGCcMath path;
    public float ratio_increment = 0.1f;
    public float min_distance = 1.0f;

    Vector3 target;
    float soFar = 0.0f;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();

        // TODO 1: Calculate the closest point from the tank to the curve
        target = path.CalcPositionByClosestPoint(transform.position);
    }

    // Update is called once per frame
    void Update () 
	{
		// TODO 2: Check if the tank is close enough to the desired point
		// If so, create a new point further ahead in the path
        if (soFar > 1.0f)
        {
            soFar -= 1.0f;
        }

        if ((target - transform.position).magnitude < min_distance)
        {
            soFar += ratio_increment;
            target = path.CalcPositionByDistanceRatio(soFar);
        }
        seek.Steer(target);
	}

	void OnDrawGizmosSelected() 
	{

		if(isActiveAndEnabled)
		{
			// Display the explosion radius when selected
			Gizmos.color = Color.green;
			// Useful if you draw a sphere were on the closest point to the path
		}

	}
}
