using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    public bool IsRotating = false;
    
    private void Awake()
    {
        
    }
    public void GetRotationLeft()
    {
        var toRotate = transform.localRotation;
        Quaternion targetRotation = toRotate * Quaternion.Euler(0, 0, 90);
        if (IsRotating == false)
        {
            AudioManager.Instance.PlaySound("Swoosh");
            StartCoroutine(RotateGameObject(targetRotation, gameObject.transform, _rotateSpeed));
        }
    }

    public void GetRotationRight()
    {
        var toRotate = transform.localRotation;
        Quaternion targetRotation = toRotate * Quaternion.Euler(0, 0, -90);
        if (IsRotating == false)
        {
            AudioManager.Instance.PlaySound("Swoosh");
            StartCoroutine(RotateGameObject(targetRotation, gameObject.transform, _rotateSpeed));
        }
    }
    
    public void GetRotationUp()
    {
        var toRotate = transform.localRotation;
        var targetRotation = toRotate * Quaternion.Euler(0, 90, 0);
        if (IsRotating == false)
        {
            Debug.Log("Hi");
            AudioManager.Instance.PlaySound("Swoosh");
            StartCoroutine(RotateGameObject(targetRotation, gameObject.transform, _rotateSpeed));
        }
    }
    
    public void GetRotationDown()
    {
        var toRotate = transform.localRotation;
        var targetRotation = toRotate * Quaternion.Euler(0, -90, -90);
        if (IsRotating == false)
        {
            AudioManager.Instance.PlaySound("Swoosh");
            StartCoroutine(RotateGameObject(targetRotation, gameObject.transform, _rotateSpeed));
        }
    }

    public void StartRandomRotation()
    {
        var random = Random.Range(0, 4);
        if (random == 1)
        {
            GetRotationRight();
        }

        if (random == 2)
        {
            GetRotationLeft();
        }

        if (random == 3)
        {
            GetRotationDown();
        }

        if (random == 4)
        {
            GetRotationUp();
        }
    }
    public IEnumerator RotateGameObject(Quaternion target,Transform trans,float rotateTime)
    {
        IsRotating = true;
        var m_rotateTime = rotateTime;
        float timer = 0;
        var start = trans.transform.rotation;
        while (timer < m_rotateTime)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / m_rotateTime, 1);

            trans.transform.rotation = Quaternion.Slerp(start, target, percentage);
            yield return null;
        }
        IsRotating = false;
    }

    
}
