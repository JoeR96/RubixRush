using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : BaseSpawner
{


    protected override void OnEnable()
    {
        SpawnFloorEmpty();
    }
    // Update is called once per frame
    private void Update()
    {
        base.Update();
    }
}
