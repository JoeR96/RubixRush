using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAchievement : AchievementManager
{
    public RunnerAchievement()
    {
        target = 1000000f;
        
    }

    private void Awake()
    {
        value = _data.ScoreCount;
    }
}
