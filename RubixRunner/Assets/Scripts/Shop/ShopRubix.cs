using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRubix : MonoBehaviour
{    
    private MeshRenderer _meshRender;
    // Start is called before the first frame update
    void Start()
    {
        _meshRender = GetComponent<MeshRenderer>();
        TestFunction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TestFunction()
    {
        foreach(var material in _meshRender.sharedMaterials)
            Debug.Log(material.name);
    }
}
