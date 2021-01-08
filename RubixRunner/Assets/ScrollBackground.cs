using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    void Update()
    {
        Vector2 offSet = new Vector2(0,Time.time * _speed);
    }
}
