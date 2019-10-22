using UnityEngine;
using System.Collections;

public class SteeringAlign : MonoBehaviour {

	public float min_angle = 0.01f;
	public float slow_angle = 0.1f;
	public float time_to_accel = 0.1f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
        // TODO 7: Very similar to arrive, but using angular velocities
        // Find the desired rotation and accelerate to it
        // Use Vector3.SignedAngle() to find the angle between two directions

        float desired = Vector3.SignedAngle(transform.forward, move.movement, Vector3.up);

        if (Mathf.Abs(desired) < slow_angle)
        {
            if (Mathf.Abs(desired) < min_angle)
            {
                move.SetRotationVelocity(0.0f);
            }
            else
            {
                float slowFactor = desired / slow_angle;
                desired *= desired / slow_angle;
            }
        }

        desired = (desired - move.rotation) / time_to_accel;

        Mathf.Clamp(desired, -move.max_rot_acceleration, move.max_rot_acceleration);

        move.AccelerateRotation(desired);
    }
}
