using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRubiksCube : Pickup
{
    public PickupRubiksCube()
    {
        type = PoolObjectType.PICKUPSCORE;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        scoreSystem.AddCoinToCounter();
        scoreSystem.AddScore();
    }
}
