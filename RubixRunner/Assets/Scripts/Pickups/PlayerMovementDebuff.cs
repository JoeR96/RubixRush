using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDebuff : Pickup
{
    public PlayerMovementDebuff()
    {
        type = PoolObjectType.PICKUPSLOWPLAYER;
        target = 2f;
    }

    protected override void ApplyEffect()
    {
       // spawner.SpawnerSpeedDown();
        //Debug.Log(spawner.move);
    }
}
