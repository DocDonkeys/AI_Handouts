using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;

public class PosChange : MonoBehaviour
{
    public float laps_per_time;
    public BGCcMath path;
    private float soFar;

    // Start is called before the first frame update
    void Start()
    {
        soFar = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        soFar += laps_per_time * Time.deltaTime;

        if (soFar > 1.0f)
        {
            soFar = 0.0f;
        }

        transform.position = path.CalcPositionByDistanceRatio(soFar);

        //path.CalcPositionByClosestPoint(soFar);
        //path.GetDistance();
        //path.CalcPositionByDistanceRatio(soFar);
    }
}
