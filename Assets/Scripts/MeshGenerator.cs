using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MeshGenerator : MonoBehaviour
{
    [SerializeField]
    private int segmentCount = 6;
    
    [SerializeField, Range(0, 1)]
    private float radius = 0.2f;



    private Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    public void GenerateMesh(List<Vector3> points)
    {

        int vertexCount = points.Count * segmentCount;
        int triangleCount = (points.Count - 1) * segmentCount * 2;
        List<Vector3> vertices = new List<Vector3>(vertexCount);

        int[] triangles = new int[triangleCount * 3];


        float segmentAngle = Mathf.PI * 2 / segmentCount;

        for (int i = 0; i < points.Count; i++)
        {

            float currentAngle = 0f;
            for (int j = 0; j < segmentCount; j++)
            {

                Vector3 vertex = new Vector3(points[i].x, Mathf.Sin(currentAngle) * radius + points[i].y, Mathf.Cos(currentAngle) * radius + points[i].z);

                //if (i > 0)
                //{
                //    Vector3 normal = points[i-1] - points[i];
                //    float angle = Vector3.Angle(normal, Vector3.up);
                //    Quaternion rotation = new Quaternion();
                //    rotation.eulerAngles = new Vector3(0, 0, angle-90);

                //    vertex = rotation * (vertex - points[i-1]) + points[i-1];
                //}

                vertices.Add(vertex);
                currentAngle -= segmentAngle;
            }
        }


        for (int i = 0; i < triangleCount / 2; i++)
        {
            int j = i * 6;
            if ((i + 1) % segmentCount == 0)
            {
                triangles[j] = i;
                triangles[j + 1] = i + 1;
                triangles[j + 2] = i + segmentCount;
                triangles[j + 3] = i - segmentCount + 1;
                triangles[j + 4] = i + 1;
                triangles[j + 5] = i;
                continue;
            }
            triangles[j] = i;
            triangles[j + 1] = i + segmentCount + 1;
            triangles[j + 2] = i + segmentCount;
            triangles[j + 3] = i + 1;
            triangles[j + 4] = i + segmentCount + 1;
            triangles[j + 5] = i;

        }


        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }


}
