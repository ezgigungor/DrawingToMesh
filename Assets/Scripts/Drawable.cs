using UnityEngine;




public class Drawable : MonoBehaviour
{
    public GameObject LinePrefab;
    public MeshObject MeshObject;
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
        if (!IsDrawingOnBoard())
            return;

        if (_prevLine)
            Destroy(_prevLine);

        _currentLine = Instantiate(LinePrefab, transform.position, Quaternion.identity).GetComponent<Line>();
    }

    private void Draw()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject == this.gameObject)
                _currentLine.AddPoint(hit.point);
        
    }

    private void EndDraw()
    {
         if(_currentLine == null)
            return;

        Mesh mesh = MeshGenerator.Instance.GenerateMesh(_currentLine.Points);
        MeshObject.ResetMesh(mesh);
        _prevLine = _currentLine.gameObject;
        _currentLine = null; 
    }


    private bool IsDrawingOnBoard()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            return hit.transform.gameObject == this.gameObject;
        return false;
    }
}
