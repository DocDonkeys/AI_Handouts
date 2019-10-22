using UnityEngine;
using System.Collections;

// TODO 2: Agents must avoid any collider in their way
// 1- Create your own (serializable) class for rays and make a public array with it[System.Serializable]
[System.Serializable]
public class My_Ray
{
    public float length;
    public Vector3 direction = Vector3.forward;
    public Vector3 offset = Vector3.zero;   // Default is object center, but you can offset the rays to other origin points
}

public class SteeringObstacleAvoidance : MonoBehaviour {

	public LayerMask mask;
	public float avoid_distance = 5.0f;
    public My_Ray[] rayArr;

    Move move;
	SteeringSeek seek;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();
    }

    // Update is called once per frame
    void Update()
    {
        //// 2- Calculate a quaternion with rotation based on movement vector
        Quaternion quat = Quaternion.AngleAxis(Mathf.Atan2(move.movement_vel.x, move.movement_vel.z) * Mathf.Rad2Deg, Vector3.up);

        // 3- Cast all rays. If one hit, get away from that surface using the hitpoint and normal info
        for (int i = 0; i < rayArr.Length; i++)
        {
            RaycastHit hit;
            Vector3 origin = transform.position + quat * rayArr[i].offset;

            if (Physics.Raycast(origin, quat * rayArr[i].direction.normalized, out hit, rayArr[i].length, mask))
                seek.Steer(new Vector3(hit.point.x, transform.position.y, hit.point.z) + hit.normal * avoid_distance);  // We don't use hit.point.y because we're moving in a plane, for 3D movement it should change
        }
    }

    void OnDrawGizmosSelected() 
	{
		if(move && this.isActiveAndEnabled)
		{
			Gizmos.color = Color.red;
			Quaternion quat = Quaternion.AngleAxis(Mathf.Atan2(move.movement_vel.x, move.movement_vel.z) * Mathf.Rad2Deg, Vector3.up);

            // 4- Make sure there is debug draw for all rays (below in OnDrawGizmosSelected)
            // TODO 2: Debug draw thoise rays (Look at Gizmos.DrawLine)
            for (int i = 0; i < rayArr.Length; i++)
            {
                Vector3 origin = transform.position + quat * rayArr[i].offset;
                Gizmos.DrawLine(origin, origin + (quat * rayArr[i].direction.normalized) * rayArr[i].length);
            }
        }
    }
}
