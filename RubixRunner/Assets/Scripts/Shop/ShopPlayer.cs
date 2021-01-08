using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayer : MonoBehaviour
{


    [SerializeField] private DataScriptableObject _data;
    [SerializeField] private GameObject _shop;
    public String PlayerColour;
    public float RotationAngle;
    public Color Colour;
    public bool IsPurchased;
    
    private Animator _animator;
    private readonly string _isWaving = "IsWaving";
    private readonly string _isDancing = "IsDancing";
    private readonly string _isBought = "IsBought";
    private string _currentAnim = "";



    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            
            _animator.SetBool(_isWaving, true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _animator.SetBool(_isDancing, true);
        }
        if (Input.GetKey(KeyCode.T))
        {
            _animator.SetBool(_isBought, true);
        }
        
    }

    private void DisableAnimation(string animation)
    {
        _animator.SetBool(animation,false);
    }

    public void BuySkin()
    {
        IsPurchased = true;
    }

}
