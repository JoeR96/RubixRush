using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _rotationSpeed;

    public RotationHash[] FacePositions;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0.75f,0.75f,0);
    }

    
    [Serializable]
    public struct RotationHash
    {
        public string name;
        public Quaternion rotations;
    }
}
