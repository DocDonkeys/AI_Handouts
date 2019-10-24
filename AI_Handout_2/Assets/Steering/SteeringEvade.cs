using UnityEngine;
using System.Collections;

public class SteeringEvade : MonoBehaviour
{
    public float max_seconds_prediction;

    Move move;
    SteeringFlee flee;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        flee = GetComponent<SteeringFlee>();
    }

    // Update is called once per frame
    void Update()
    {
        Steer(move.target.transform.position, move.target.GetComponent<Move>().movement, move.target.GetComponent<Move>().max_mov_speed);
    }

    public void Steer(Vector3 target_position, Vector3 target_velocity, float max_target_speed)
    {
        float time_span = (target_position - transform.position).magnitude / max_target_speed;

        if (time_span > max_seconds_prediction)
            time_span = max_seconds_prediction;

        Vector3 future_target = target_position + target_velocity * time_span;
        flee.Steer(future_target);
    }
}
