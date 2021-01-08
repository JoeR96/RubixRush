using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
[Serializable]
public class DataScriptableObject : ScriptableObject
{
    private Dictionary<string,GameObject> _playerSkins = new Dictionary<string, GameObject>();
    private GameObject _skinHolder;
    public bool IsInitalised;
    public bool tutorialMode;
    public float ScoreCount;
    [SerializeField] public GameObject[] Player;
    [SerializeField] public float CoinsCollected;
    public float Distancetravelled;

    [SerializeField] public GameObject _activeSkin;

    private void Awake()
    {
        IsInitalised = false;
    }

    
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

    public void FillDictionary()
    {
        _skinHolder = GameObject.FindWithTag("SkinHolder");
        foreach (Transform child in _skinHolder.transform)
        {
            _playerSkins.Add(child.name,child.gameObject);
        }
    }

    public void UnlockSkin(string colour)
    {
        _playerSkins[colour].GetComponent<ShopPlayer>().IsPurchased = true;
    }

    public bool IsSkinBought(string colour)
    {
        if (_playerSkins[colour].GetComponent<ShopPlayer>().IsPurchased)
            return true;

        return false;
    }

    public void ReturnSkin(string colour)
    {
        foreach (var go in Player)
        {
            if (go.name == colour)
            {
                SetSkinActive(go);
            }
        }
    }
    
    public void SetSkinActive(GameObject go)
    {
        _activeSkin = go;
    }

    public void SetSkin()
    {
        
    }

}
