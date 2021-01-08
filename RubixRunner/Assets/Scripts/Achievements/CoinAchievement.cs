using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAchievement : AchievementManager
{
    public CoinAchievement()
    {
        target = 1000f;
    }

    private void Awake()
    {
        value = _data.CoinsCollected;
    }
}
