using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]private DataScriptableObject scoreData;
    public float Score { get; private set; }
    public float Coins { get; private set; }
    
    private readonly float[] _multiplierValues = new float[5] {0,2,4,6,8};
    private float _currentMultiplierValue;
    private int ArrayPointer { get; set; } = 0;
    private readonly float _buffDuration = 4f;
    private float _buffTimer = 0;
    private bool IsLerping;

    private void Start()
    {
        _currentMultiplierValue = _multiplierValues[0];
    }
    public void AddScore()
    {
        var toAdd = 50 * _multiplierValues[ArrayPointer];
        Score += toAdd;
        scoreData.AddScore(toAdd);
    }

    private void Update()
    {
        // if (_buffTimer > 0)
        // {
        //     _buffTimer -= Time.deltaTime;
        // }
        //
        // if (_buffTimer <= 0)
        // {
        //     RemoveBuff();
        // }

        if (Input.GetKey(KeyCode.F1))
        {
            ArrayPointer = 0;
        }
        if (Input.GetKey(KeyCode.F2))
        {
            AddBuff();
            ArrayPointer = 1;
        }
        if (Input.GetKey(KeyCode.F3))
        {
            AddBuff();
            ArrayPointer = 2;
        }
        if (Input.GetKey(KeyCode.F4))
        {
            AddBuff();
            ArrayPointer = 3;
        }
        if (Input.GetKey(KeyCode.F5))
        {
            AddBuff();
            ArrayPointer = 4;
        }
        if (Input.GetKey(KeyCode.F6))
        {
            RemoveBuff();
        }
    }

    private void RefreshBuff()
    {
        _buffTimer = _buffDuration;
    }

    private void RemoveBuff()
    {
        ArrayPointer = 0;
        StartCoroutine(LerpValue(_multiplierValues[ArrayPointer]));
    }
    
    public void AddBuff()
    {
        _buffTimer = _buffDuration;
        if (ArrayPointer < 4)
        {
            ArrayPointer++;
            if (!IsLerping)
            {
                StartCoroutine(LerpValue(_multiplierValues[ArrayPointer]));
            }
            
        }
        else if(ArrayPointer == 4)
        {
            RefreshBuff();
        }
    }

    public float GetScoreMultiplier()
    {
        var toReturn = _currentMultiplierValue;
        return toReturn;
    }

    public string GetScore()
    {
        var toReturn = Score.ToString();
        return toReturn;
    }

    public string GetCoins()
    {
        var toReturn = Score.ToString();
        return toReturn;
    }
    public void AddCoinToCounter()
    {
        scoreData.AddCoin();
        Coins++;
    }
    
    public IEnumerator LerpValue(float target)
    {
        IsLerping = true;
        var increaseTime = 0.5f;
        var start = GetScoreMultiplier();
        float timer = 0f;
        while (timer < increaseTime)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / increaseTime, 1);
            _currentMultiplierValue = Mathf.Lerp(start, target, percentage);
            yield return null;
        }
        IsLerping = false;
    }
}

