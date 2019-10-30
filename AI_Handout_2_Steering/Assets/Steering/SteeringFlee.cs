using UnityEngine;
using System.Collections;

public class SteeringFlee : MonoBehaviour {

	Move move;

	// Use this for initialization
	void Start () {
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

        //TODO 2: Same as Steering seek but opposite direction
        Vector3 diff = move.target.transform.position - transform.position;

        diff.x *= -1.0f;
        diff.y *= -1.0f;
        diff.z *= -1.0f;

        move.AccelerateMovement(diff);
    }
}
