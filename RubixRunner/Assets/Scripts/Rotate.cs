using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate( 90 * Time.deltaTime * 5f,0f, 0f ) ;
    }
}
