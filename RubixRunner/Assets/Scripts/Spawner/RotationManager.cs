using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    private bool isRotating = false;
    Quaternion targetRotation;
    public AudioSource RotationAudio;

    private void Awake()
    {
        RotationAudio = GetComponent<AudioSource>();
    }
    public void GetRotationLeft()
    {
        var toRotate = transform.localRotation;
        targetRotation = toRotate * Quaternion.Euler(0, 0, 90);
        if (isRotating == false)
        {
            RotationAudio.Play();
            StartCoroutine(nameof(Rotate), targetRotation);
        }
    }

    public void GetRotationRight()
    {
        var toRotate = transform.localRotation;
        targetRotation = toRotate * Quaternion.Euler(0, 0, -90);
        if (isRotating == false)
        {
            RotationAudio.Play();
            StartCoroutine(nameof(Rotate), targetRotation);
        }
    }

    public IEnumerator Rotater(Quaternion target)
    {
        isRotating = false;
        float time = 1f;
        float i = 0;
        float rate = 360f / time;
        var start = transform.rotation;
        while (i < 180)
        {
            isRotating = true;
            i += Time.deltaTime * rate;

            transform.rotation = Quaternion.RotateTowards(start, target, i);
            yield return null;
        }
        isRotating = false;
    }

    public IEnumerator Rotate(Quaternion target)
    {
        isRotating = true;
        var m_rotateTime = 0.2f;
        float timer = 0;
        var start = transform.rotation;
        while (timer < m_rotateTime)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / m_rotateTime, 1);

            transform.rotation = Quaternion.Slerp(start, target, percentage);
            yield return null;
        }
        isRotating = false;
    }
    
    public IEnumerator RotateGameObject(Quaternion target,Transform trans,float rotateTime)
    {
        isRotating = true;
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
        isRotating = false;
    }

    
}
