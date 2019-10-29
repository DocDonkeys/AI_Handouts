using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPercieve : MonoBehaviour
{
    public LayerMask targets;
    public LayerMask obstacles;

    [Header("Contact")]
    public bool contact = true;
    public float radius = 5.0f;

    [Header("Hearing")]
    public bool hearing = true;
    public float distance = 15.0f;

    [Header("Sight")]
    public bool sight = true;
    public Camera vision;

    private float visionDistance;
    private float colliderSearchRadius;

    private List<GameObject> detected = new List<GameObject>();
    private Collider myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        vision = GetComponent<Camera>();

        visionDistance = vision.farClipPlane / Mathf.Cos(vision.fieldOfView / 2.0f);  // Make cone radius out of FOV angle and farClipPlane distance from camera

        colliderSearchRadius = Mathf.Max(visionDistance, distance);
        colliderSearchRadius = Mathf.Max(colliderSearchRadius, radius); // Use the largest area of perception for collecting in a sphere
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, colliderSearchRadius, targets);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(vision);

        List<PerceptionEvent> currently_percieving = new List<PerceptionEvent>();   // Record of all perceived objects and the method used for detection

        foreach (Collider col in colliders)
        {
            if (col != myCollider) // If collider isn't from the agent who's scanning
            {
                PerceptionEvent foundData = new PerceptionEvent();
                Vector3 targetVector = col.transform.position - transform.position;
                float targetDistance = targetVector.magnitude;

                bool inRange = false;

                if (sight && GeometryUtility.TestPlanesAABB(planes, col.bounds))   // In vision FOV
                {
                    foundData.sense = PerceptionEvent.senses.VISION;
                    inRange = true;
                }
                else if (hearing && targetDistance < distance)    // In hearing range
                {
                    foundData.sense = PerceptionEvent.senses.SOUND;
                    inRange = true;
                }
                else if (contact && targetDistance < radius)  // In contact distance
                {
                    foundData.sense = PerceptionEvent.senses.CONTACT;
                    inRange = true;
                }

                if (inRange && !Physics.Raycast(transform.position, targetVector, targetDistance, obstacles))  // If no obstacles in the way
                {
                    foundData.go = col.gameObject;
                    currently_percieving.Add(foundData);   // We list all that we percieve
                }
            }
        }
        
        // The following process consists in matching both lists and pair their objects, the ones that remain unpaired must be updated in the "detected" list
        bool[] detectedgMatch = new bool[detected.Count];
        bool[] percievingMatch = new bool[currently_percieving.Count];  // These arrays will flag the pairing of the content in the same index positions
                                                                        // Ask Marc: We could use a "index list" for the same purpose, should we?
        for (int i = 0; i < detected.Count; i++)
        {
            for (int j = 0; j < currently_percieving.Count; j++)
            {
                if (detected[i] == currently_percieving[i].go)  // If objects match in both lists, it means what I remember what I just percieved
                {
                    detectedgMatch[i] = true;   // We flag the booleans at their corresponding indexes
                    percievingMatch[j] = true;
                    break;
                }
            }
        }

        // We iterate the boolean arrays, were unpaired objects are marked with false flags
        for (int i = detected.Count - 1; i >= 0; i--)
        {
            if (detectedgMatch[i] == false) // If an object that was remembered to be detected isn't percieved currently, it is LOST
            {
                PerceptionEvent lostData = new PerceptionEvent();
                lostData.go = detected[i];
                lostData.type = PerceptionEvent.types.LOST;
                SendMessage("PerceptionEvent", lostData);
                detected.RemoveAt(i);
            }
        }

        for (int i = currently_percieving.Count - 1; i >= 0; i--)   // If a currently percieved object isn't remembered, we need to add it to "memory" as NEW
        {
            if (percievingMatch[i] == false)
            {
                currently_percieving[i].type = PerceptionEvent.types.NEW;
                SendMessage("PerceptionEvent", currently_percieving[i]);
                detected.Add(currently_percieving[i].go);
            }
        }
    }
}
