using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPercieve : MonoBehaviour
{
    public LayerMask mask;

    [Header("Area")]
    public bool area = true;
    public float radius = 5.0f;

    [Header("Cone")]
    public bool cone = true;
    public Camera vision;

    private Collider myCollider;
    public float visionDistance;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        vision = GetComponent<Camera>();

        visionDistance = vision.farClipPlane / Mathf.Cos(vision.fieldOfView / 2.0f);  // Make cone radius out of FOV angle and farClipPlane distance from camera
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, visionDistance, mask);

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(vision);

        for (int i = 0; i < colliders.Length; i++)
        {
            bool ret = false;

            if (colliders[i] != myCollider)
            {
                if (area && Vector3.Magnitude(colliders[i].transform.position - transform.position) < radius)
                    ret = true;

                if (cone && GeometryUtility.TestPlanesAABB(planes, colliders[i].bounds))
                    ret = true;
            }

            if (ret)
            {
                Debug.Log("EnemySighted!");
            }
        }
    }
}
