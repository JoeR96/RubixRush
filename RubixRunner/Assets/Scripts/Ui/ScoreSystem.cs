using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]private DataScriptableObject scoreData;
    public float Score { get; private set; }
    
    public int[] multiplier = new int[4] { 1,2,4,8};
    private int ArrayPointer { get; set; } = 0;
    private float buffDuration = 4f;
    private float buffTimer = 0;
    public void AddScore()
    {
        var toAdd = 200 * multiplier[ArrayPointer];
        Score += toAdd;
        scoreData.AddScore(toAdd);
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

    public int GetScoreMultiplier()
    {
        var toReturn = multiplier[ArrayPointer];
        return toReturn;
    }

    public string GetScore()
    {
        var toReturn = Score.ToString();
        return toReturn;
    }

    public void AddCoinToCounter()
    {
        scoreData.AddCoin();
    }
}

