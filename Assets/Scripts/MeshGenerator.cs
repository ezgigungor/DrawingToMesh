using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MeshGenerator : MonoBehaviour
{
    [SerializeField]
    private int _segmentCount = 6;
    
    [SerializeField, Range(0, 1)]
    private float _radius = 0.2f;


    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
    }
    public void GenerateMesh(List<Vector3> points)
    {

        int vertexCount = points.Count * _segmentCount;
        int triangleCount = (points.Count - 1) * _segmentCount * 2;
        List<Vector3> vertices = new List<Vector3>(vertexCount);

        int[] triangles = new int[triangleCount * 3];


        float segmentAngle = 2 * Mathf.PI / _segmentCount;

        for (int i = 0; i < points.Count; i++)
        {

            float currentAngle = 0f;

            Vector3 direction = Vector3.forward;
            Vector3 center = points[i];

            if (i > 0)
            {
                Vector3 previous = points[i - 1];
                direction = (center - previous);
            }
            if (i < points.Count - 2)
            {
                Vector3 next = points[i + 1];
                direction += (next - center);
            }

            direction.Normalize();
            Quaternion facingDirection = Quaternion.LookRotation(direction, direction);

            for (int j = 0; j < _segmentCount; j++)
            {
                
                Vector3 offset = new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle), 0) * _radius;
                vertices.Add(center + (facingDirection * offset));

                if (i == 1)
                    vertices[((i-1) * _segmentCount) + j] = points[i-1] + (facingDirection * offset);

                currentAngle -= segmentAngle;

                
            }

            

        }


        for (int i = 0; i < triangleCount / 2; i++)
        {
            int j = i * 6;
            if ((i + 1) % _segmentCount == 0)
            {
                triangles[j] = i;
                triangles[j + 1] = i + 1;
                triangles[j + 2] = i + _segmentCount;
                triangles[j + 3] = i - _segmentCount + 1;
                triangles[j + 4] = i + 1;
                triangles[j + 5] = i;
            }else
            {
                triangles[j] = i;
                triangles[j + 1] = i + _segmentCount + 1;
                triangles[j + 2] = i + _segmentCount;
                triangles[j + 3] = i + 1;
                triangles[j + 4] = i + _segmentCount + 1;
                triangles[j + 5] = i;
            }
        }

        _mesh.Clear();
        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }

}
