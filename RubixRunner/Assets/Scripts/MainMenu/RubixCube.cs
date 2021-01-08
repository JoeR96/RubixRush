using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubixCube : MonoBehaviour
{
    [SerializeField] private bool _ScaleCube;
    private bool _isRotating;

    private RotationManager _rotationManager;

    private void Awake()
    {
        _rotationManager = GetComponent<RotationManager>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _isRotating = false;
        if (_ScaleCube)
        {
            StartCoroutine(RotateGameObject());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_rotationManager.IsRotating)
        {
            _rotationManager.StartRandomRotation();
        }
    }
    
    public IEnumerator RotateGameObject()
    {
        var scaleTime = 0.5f;
        float timer = 0;
        var start = transform.localScale;
        while (timer < scaleTime)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / scaleTime, 1);
            transform.localScale = Vector3.Lerp(start, transform.localScale * 50, percentage); 

            yield return null;
        }
    }
}
