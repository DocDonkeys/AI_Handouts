using UnityEngine;
using System.Collections;

public class SteeringVelocityMatching : MonoBehaviour {

	public float time_to_accel = 0.25f;

	Move move;
	Move target_move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		target_move = move.target.GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(target_move)
		{
            // TODO 8: First come up with your ideal velocity
            // then desirederate to it.
            Vector3 desired = target_move.movement / time_to_accel;

            desired.x = Mathf.Clamp(desired.x, -move.max_mov_acceleration, move.max_mov_acceleration);
            desired.y = Mathf.Clamp(desired.y, -move.max_mov_acceleration, move.max_mov_acceleration);
            desired.z = Mathf.Clamp(desired.z, -move.max_mov_acceleration, move.max_mov_acceleration);

            move.AccelerateMovement(desired);
        }
	}
}
