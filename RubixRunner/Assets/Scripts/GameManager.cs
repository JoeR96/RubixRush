using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        STAGEONE,
        STAGETWO,
        STAGETHREE,
        STAGEFOUR,
        STAGEFIVE,
        STAGESIX,
        STAGESEVEN,
        GAME
    }

    private GameState gameState;
}
