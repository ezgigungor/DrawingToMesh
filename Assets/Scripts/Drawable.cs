using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Drawable : MonoBehaviour
{

    public GameObject linePrefab;
    public MeshGenerator meshGenerator;
    
    private Line _currentLine;
    private GameObject _prevLine;
    private Camera _camera;


    private void Start()
    {
        _camera = Camera.main;
        
    }

    private void Update()
    { 

        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    private void FixedUpdate()
    {
        if (_currentLine != null)
            Draw();
    }


    private void BeginDraw()
    {
        if (_prevLine)
            Destroy(_prevLine);

        _currentLine = Instantiate(linePrefab, transform.position, Quaternion.identity).GetComponent<Line>();
    }

    private void Draw()
    {
        Ray ray  = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
       
        if (Physics.Raycast(ray, out hit))
            _currentLine.AddPoint(hit.point);
        
    }

    private void EndDraw()
    {
         meshGenerator.GenerateMesh(_currentLine.GetPoints());
        _prevLine = _currentLine.gameObject;
        _currentLine = null; 
    }
}
