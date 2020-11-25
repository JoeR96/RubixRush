using UnityEngine;

namespace Pickups
{
    public class PlayerMovementBuff : Pickup
    {
        public PlayerMovementBuff()
        {
            type = PoolObjectType.PICKUPFASTPLAYER;
            target = 2f;
        }

        protected override void ApplyEffect()
        {
            spawner.SpawnerSpeedDown();
        }
    }
}
