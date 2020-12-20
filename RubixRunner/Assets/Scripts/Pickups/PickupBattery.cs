using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBattery : Pickup
{
    public PickupBattery()
    {
        type = PoolObjectType.OBSTACLE;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        scoreSystem.AddCoinToCounter();
    }
}