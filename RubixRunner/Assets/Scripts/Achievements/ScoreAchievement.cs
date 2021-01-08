using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAchievement : AchievementManager
{
    // Start is called before the first frame update
    public ScoreAchievement()
    {
        target = 1000f;
        
    }

    private void Awake()
    {
        value = _data.ReturnScore();
    }
}
