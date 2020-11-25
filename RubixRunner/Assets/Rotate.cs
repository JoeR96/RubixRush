using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(10 * Time.deltaTime * 2f,0, 0 ) ;
    }
}
