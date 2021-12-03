using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshObject : MonoBehaviour
{
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private MeshCollider _meshCollider;
    private MeshFilter _meshFilter;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _originalPosition = transform.position;
        _meshCollider = GetComponent<MeshCollider>();
        _meshFilter = GetComponent<MeshFilter>();      
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ResetMesh(Mesh mesh)
    {
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;
        _meshFilter.mesh = mesh;
        _rigidbody.useGravity = true;
        _meshCollider.sharedMesh = mesh;
    }

}
