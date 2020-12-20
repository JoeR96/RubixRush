using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
[Serializable]
public class DataScriptableObject : ScriptableObject
{
    public bool tutorialMode { get; private set; }
    public float ScoreCount { get; private set; }
    public float CoinsCollected { get; private set; }
    public float Distancetravelled { get; private set; }

    public void ToggleTutorialMode()
    {
        tutorialMode = !tutorialMode;
    }

    public bool GetTutorialMode()
    {
        return tutorialMode;
    }
    public void AddScore(float score)
    {
        ScoreCount += score;
    }

    public float ReturnScore()
    {
        return ScoreCount;
    }

    public void AddCoin()
    {
        CoinsCollected += 1;
    }

    public float ReturnCoinCount()
    {
        return CoinsCollected;
    }

    public void AddDistance(float distance)
    {
        Distancetravelled += distance;
    }

    public float ReturnDistance()
    {
        return CoinsCollected;
    }
}
