using UnityEngine;
using System.Collections;

public class SteeringSeek : MonoBehaviour {

    public float time_to_accel;
	Move move;
    
	// Use this for initialization
	void Start ()
    {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Steer(move.target.transform.position);
	}

	public void Steer(Vector3 target)
	{
        if (!move)
            move = GetComponent<Move>();

        // TODO 1: accelerate towards our target at max_acceleration
        // use move.AccelerateMovement()
        Vector3 desired = (target - transform.position).normalized * move.max_mov_speed;

        Vector3 accel = (desired - move.movement) / time_to_accel;

        accel.x = Mathf.Clamp(accel.x, -move.max_mov_acceleration, move.max_mov_acceleration);
        accel.y = Mathf.Clamp(accel.y, -move.max_mov_acceleration, move.max_mov_acceleration);
        accel.z = Mathf.Clamp(accel.z, -move.max_mov_acceleration, move.max_mov_acceleration);

        move.AccelerateMovement(accel);
    }
}
