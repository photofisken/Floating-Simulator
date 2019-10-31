using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeCalculator
{
    public static float GetVolume(int mediumType, Mesh mesh, Transform meshTransform, Vector3 intersectPosition)
    {
        float volume = 0;
        List<MyTriangle>[] triangleLists = Intersection.GetTriangleLists(mesh, meshTransform, intersectPosition);
        List<MyTriangle> overWaterTriangles = triangleLists[0];
        List<MyTriangle> underWaterTriangles = triangleLists[1];

        switch (mediumType) // Return volume for either mesh under or over water
        {

            case 0:
                for (int i = 0; i < overWaterTriangles.Count; i++)
                {
                    Vector3 p1 = overWaterTriangles[i].vertices[0].position;
                    Vector3 p2 = overWaterTriangles[i].vertices[1].position;
                    Vector3 p3 = overWaterTriangles[i].vertices[2].position;

                    volume += SignedVolumeOfTriangle(p1, p2, p3);
                }
                break;
                
            case 1:
                for (int i = 0; i < underWaterTriangles.Count; i++)
                {
                    Vector3 p1 = underWaterTriangles[i].vertices[0].position;
                    Vector3 p2 = underWaterTriangles[i].vertices[1].position;
                    Vector3 p3 = underWaterTriangles[i].vertices[2].position;

                    volume += SignedVolumeOfTriangle(p1, p2, p3);
                }
                break;
        }
        return volume;
    }

    public static float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }
}
