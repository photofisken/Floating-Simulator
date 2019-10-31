using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    public float weight;

    //Not adjustable factors (for now)
    private  Mesh mesh;
    [HideInInspector] public bool dragging = false;

    private void Start()
    {
        mesh = GetComponent<Mesh>();
    }
    private void FixedUpdate()
    {
        if (!dragging)
        {
            ApplyForce(AddForces());
        }
    }
    public Vector3 AddForces()
    {
        Vector3 resultant = new Vector3();
        resultant = Gravity.CalculateGravity(weight) + Lift.CalculateLift(mesh);
        return resultant;
    }

    public void ApplyForce(Vector3 resultant)
    {
        transform.position += resultant * Time.fixedDeltaTime;
    }
}
