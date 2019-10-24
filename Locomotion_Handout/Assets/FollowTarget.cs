using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    NavMeshAgent agent;
    Camera camera;
    Animator anim;
    public GameObject target;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        camera = GetComponent<Camera>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("move", agent.SetDestination(target.transform.position));

        anim.SetFloat("vel x", agent.velocity.x);
        anim.SetFloat("vel y", agent.velocity.z);
    }
}
