using UnityEngine;
using System.Collections;

public class SteeringPursue : MonoBehaviour {

	public float max_seconds_prediction;

	Move move;
    SteeringArrive arrive;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
        arrive = GetComponent<SteeringArrive>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Steer(move.target.transform.position, move.target.GetComponent<Move>().movement, move.target.GetComponent<Move>().max_mov_speed);
	}

	public void Steer(Vector3 target_position, Vector3 target_velocity, float max_target_speed)
	{
        // TODO 5: Create a fake position to represent
        // enemies predicted movement. Then call Steer()
        // on our Steering Seek / Arrive with the predicted position in
        // max_seconds_prediction time
        // Be sure that arrive / seek's update is not called at the same time
        float time_span = (target_position - transform.position).magnitude / max_target_speed;

        if (time_span > max_seconds_prediction)
            time_span = max_seconds_prediction;

        Vector3 future_target = target_position + target_velocity * time_span;
        arrive.Steer(future_target);

        // TODO 6: Improve the prediction based on the distance from
        // our target and the speed we have

    }
}
