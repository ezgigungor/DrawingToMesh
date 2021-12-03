using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{

    [SerializeField, Range(0, 1)]
    private float minDistance = 0.2f;
    private LineRenderer _lineRenderer;
    private List<Vector3> _points;
    public List<Vector3> Points { get { return _points; } }
    private void Awake()
    {
        _points = new List<Vector3>();
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
