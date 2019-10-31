using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public static Gravity instance;

    public float gForce = 9.82f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public static Vector3 CalculateGravity(float mass)
    {
        Vector3 gravityForce = new Vector3(0, instance.gForce * mass, 0);
        return gravityForce;
    }
}
