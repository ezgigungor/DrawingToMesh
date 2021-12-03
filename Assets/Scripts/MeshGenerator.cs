using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField]
    private int _segmentCount = 6;
    
    [SerializeField, Range(0, 1)]
    private float _radius = 0.2f;

    private static MeshGenerator _instance;
    public static MeshGenerator Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    public Mesh GenerateMesh(List<Vector3> points)
    {
        Mesh mesh = new Mesh();
        float segmentAngle = 2 * Mathf.PI / _segmentCount;
        int vertexCount = points.Count * _segmentCount;
        int triangleCount = (points.Count - 1) * _segmentCount * 2;
        List<Vector3> vertices = new List<Vector3>(vertexCount);
        int[] triangles = new int[triangleCount * 3];

        //Calculates the facing direction of the circle to be generated at the point
        //by averaging previous and next facing directions
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

                //Change the facing diretion of the first circle to be the same as the second one
                if (i == 1)
                    vertices[((i-1) * _segmentCount) + j] = points[i-1] + (facingDirection * offset);

                currentAngle -= segmentAngle;
            }
        }

        // Setting triangles as a cylinder between each point
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

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }

}
