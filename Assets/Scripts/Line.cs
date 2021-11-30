using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{

    private LineRenderer _lineRenderer;

    public List<Vector3> _points { get; set; } = new List<Vector3>();
    [SerializeField, Range(0, 1)]
    private float minDistance = 0.2f;


   

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    public void AddPoint(Vector3 point)
    {
        if (_lineRenderer.positionCount >= 1 && Vector2.Distance(point, _lineRenderer.GetPosition(_lineRenderer.positionCount - 1)) < minDistance)
            return;

        _points.Add(point);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, point);
        
    }
}
