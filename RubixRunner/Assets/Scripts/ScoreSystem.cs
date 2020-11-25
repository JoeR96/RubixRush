using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public float Score { get; set; }
    public int[] multiplier = new int[4] { 1,2,4,8};
    private int ArrayPointer { get; set; } = 0;
    private float buffDuration = 4f;
    private float buffTimer = 0;
    public void AddScore()
    {
        Score += (200 * multiplier[ArrayPointer]);
    }

    private void Update()
    {
        if (buffTimer > 0)
        {
            buffTimer -= Time.deltaTime;
        }

        if (buffTimer <= 0)
        {
            RemoveBuff();
        }
    }

    private void RefreshBuff()
    {
        buffTimer = buffDuration;
    }

    private void RemoveBuff()
    {
        ArrayPointer = 0;
    }
    
    public void AddBuff()
    {
        buffTimer = buffDuration;
        if (ArrayPointer < 3)
        {
            ArrayPointer++;
        }
        else if(ArrayPointer == 3)
        {
            RefreshBuff();
        }
    }

    public string GetScoreMultiplier()
    {
        var toReturn = ArrayPointer.ToString();
        return toReturn;
    }

    public string GetScore()
    {
        var toReturn = Score.ToString();
        return toReturn;
    }

}

