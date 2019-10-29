using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPercieve : MonoBehaviour
{
    public LayerMask targets;
    public LayerMask obstacles;

    [Header("Area")]
    public bool area = true;
    public float radius = 5.0f;

    [Header("Cone")]
    public bool cone = true;
    public Camera vision;

    private float visionDistance;
    private float colliderSearchRadius;

    private List<GameObject> detected;
    private Collider myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        vision = GetComponent<Camera>();

        visionDistance = vision.farClipPlane / Mathf.Cos(vision.fieldOfView / 2.0f);  // Make cone radius out of FOV angle and farClipPlane distance from camera

        if (visionDistance > radius)    // Use the largest distance from the two perceptions (Area/Vision) for collider detection
            colliderSearchRadius = visionDistance;
        else
            colliderSearchRadius = radius;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, colliderSearchRadius, targets);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(vision);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != myCollider) // If collider isn't from the agent who's scanning
            {
                PerceptionEvent eventData = null;
                Vector3 targetDistance = colliders[i].transform.position - transform.position;

                if (cone && GeometryUtility.TestPlanesAABB(planes, colliders[i].bounds))    // If sound detection ON and inside detection radius
                {
                    eventData.sense = PerceptionEvent.senses.VISION;
                }
                else if (area && targetDistance.magnitude < radius) // If visual detection ON and inside camera FOV
                {
                    eventData.sense = PerceptionEvent.senses.SOUND;
                }
                {
                    if (!Physics.Raycast(transform.position, targetDistance, targetDistance.magnitude, obstacles))  // If no obstacles in the way
                    {
                        bool already_seen = false;

                        for (int j = 0; j < detected.Count; j++)
                        {
                            if (detected[i] == colliders[i].gameObject)
                            {
                                already_seen = true;
                                break;
                            }
                        }

                        if (!already_seen)
                        {
                            //eventData.go = colliders[i].gameObject;
                            //eventData.type = PerceptionEvent.types.NEW;

                            //SendMessage(PerceptionEvent, eventData);
                        }
                    }
                }
            }
        }
    }
}
