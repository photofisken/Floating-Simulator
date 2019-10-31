using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift
{
    private float densityAir;
    private float densityWater;
    public static Vector3 CalculateLift(Mesh mesh)
    {
        // F = Volume * density * gForce
        Vector3 liftForce = LiftWater() + LiftAir();
        return Vector3.zero;
    }

    private Vector3 WaterLift()
    {

        return Vector3.zero;
    }

    private Vector3 AirLift()
    {
        return Vector3.zero;
    }
}
