using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAchievement : AchievementManager
{
    public DistanceAchievement()
    {
        target = 1000f;
        
    }

    private void Awake()
    {
        value = _data.Distancetravelled;
    }
}
