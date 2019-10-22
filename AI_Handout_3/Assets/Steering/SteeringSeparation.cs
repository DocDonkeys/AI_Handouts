using UnityEngine;
using System.Collections;

public class SteeringSeparation : MonoBehaviour {

    public LayerMask mask;
	public float search_radius = 5.0f;
    public float time_to_accel = 1.0f;
	public AnimationCurve strength;

    Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 1: Agents much separate from each other:
        // 1- Find other agents in the vicinity (use a layer for all agents)
        Collider[] colliders = Physics.OverlapSphere(transform.position, search_radius, mask);
        Vector3 repulsion_vec = Vector3.zero;

        // 2- For each of them calculate a escape vector using the AnimationCurve
        //foreach (Collider col in colliders)
        for (uint col = 0; col < colliders.Length; col++)
        {
            //GameObject gObj = col.gameObject;
            GameObject gObj = colliders[col].gameObject;

            if (gObj != gameObject)
            {
                Vector3 distance = transform.position - gObj.transform.position;
                repulsion_vec += distance.normalized * ((1.0f - strength.Evaluate(distance.magnitude / search_radius)) * move.max_mov_acceleration / time_to_accel);
            }
        }

        // 3- Sum up all vectors and trim down to maximum acceleration
        if (repulsion_vec != Vector3.zero)
        {
            float repulsion_accel = repulsion_vec.magnitude;

            if (repulsion_accel > move.max_mov_acceleration) //Limit repulsion force (ASK MARC: Not strong enough to counteract other script's accelerations, how to fix?)
                repulsion_vec = repulsion_vec.normalized * move.max_mov_acceleration;

            move.AccelerateMovement(repulsion_vec);
        }
        //else if (move.movement_vel != Vector3.zero) //WARNING: This script only accelerates the object to be repeled from others, it does not make it stop once it is far enough!
        //{
        //    move.SetMovementVelocity(Vector3.zero);
        //}
    }

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, search_radius);
	}
}
