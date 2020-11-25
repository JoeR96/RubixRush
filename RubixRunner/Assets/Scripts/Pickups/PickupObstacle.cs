using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObstacle : Pickup
{
    public PickupObstacle()
    {
        type = PoolObjectType.OBSTACLE;
    }
}
