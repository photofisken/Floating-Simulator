using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyTriangle
{
    public MyTriangle(Vector3[] points)
    {
        // For all the vertices in the triangle(3) create a MyVertex with the triangle(s) it is in
        for (int i = 0; i < Mathf.Min(points.Length, 3); i++)
        {
            MyVertex vertex = new MyVertex(points[i]);
            vertex.AddTriangle(this);
            vertices[i] = vertex;
        }

        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i].UpdateConnections();
        }
    }

    public MyVertex[] vertices = new MyVertex[3];
}

[System.Serializable]
public class MyVertex
{
    public Vector3 position;
    public List<MyVertex> connections = new List<MyVertex>();
    public List<MyTriangle> triangles = new List<MyTriangle>();

    public MyVertex(Vector3 position)
    {
        this.position = position;
    }

    public void AddTriangle(MyTriangle triangle)
    {
        triangles.Add(triangle);
    }

    public void UpdateConnections()
    {
        connections.Clear();

        // For every triangle the vertex appears in (typically 8?)
        foreach(MyTriangle triangle in triangles)
        {
            // For each vertex in the triangle the vertex appears in
            foreach(MyVertex vertex in triangle.vertices)
            {
                // Add the other teo as a connection to the vertex (neighbours)
                if (vertex != this && !connections.Contains(vertex))
                    connections.Add(vertex);
            }
        }
    }
}

public class Interection
{
    // All the converted triangles and vertices in the mesh
    List<MyTriangle> myTriangles = new List<MyTriangle>();
    List<MyVertex> myVertices = new List<MyVertex>();

    private void Start()
    {
    }

    public List<MyTriangle>[] Intersect(Mesh mesh, Transform meshTransform, Vector3 intersectPosition, Vector3 normal)
    {
        List<MyTriangle> trianglesOverWater = new List<MyTriangle>();
        List<MyTriangle> trianglesUnderWater = new List<MyTriangle>();

        // Convert the mesh vertexes into myVertexes with connections and stuff
        ConvertToTriangles(mesh);
        Matrix4x4 localToWorld = meshTransform.localToWorldMatrix;

        // For every triangle in the mesh, check its three vertices and see if average is over or under water
        for (int i = 0; i < myTriangles.Count; i++)
        {
            Vector3 averagePos = new Vector3();
            foreach (MyVertex vertex in myTriangles[i].vertices)
            {
                Vector3 world_v = localToWorld.MultiplyPoint3x4(vertex.position);               
                averagePos += world_v;
            }
            averagePos /= 3;
            // If under water, add to underWater list, else add to overWater list
            if (averagePos.y <= intersectPosition.y)
            {
                trianglesUnderWater.Add(myTriangles[i]);
            }
            else trianglesOverWater.Add(myTriangles[i]);
        }
    }
    
    public void ConvertToTriangles(Mesh mesh)
    {
        // Original vertices and triangles from the mesh
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        // Add the triangle vertices to a MyTriangle then add to list
        for(int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3[] points = new Vector3[3];
            points[0] = vertices[triangles[i + 0]];
            points[1] = vertices[triangles[i + 1]];
            points[2] = vertices[triangles[i + 2]];

            MyTriangle triangle = new MyTriangle(points);
            myTriangles.Add(triangle);

            // After convertion in MyTriangle (from point to vertex) add vertex in list
            foreach(MyVertex vertex in triangle.vertices)
            {
                myVertices.Add(vertex);
            }
        }
    }
}
