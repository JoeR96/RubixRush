using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    
    [SerializeField] private DataScriptableObject _dataContainer;

    [SerializeField] private Button _purchaseButton;
    [SerializeField] private TextMeshProUGUI _purchasedText;
    [SerializeField] private Color _currentColor;

    [SerializeField] private Light _shopLight;

    [SerializeField] private List<ShopPlayer> _shopPlayers = new List<ShopPlayer>();

    [SerializeField] private Camera _shopCamera;
    [SerializeField] private GameObject _rayOrigin;
    public int _shopPointer;
    
    private RotationManager _rotationManager;
    Quaternion _targetRotation;
    
    private bool _swipeLeft;
    private bool _isRotating;
    private float _rotationTime = 0.75f;
    

    // Start is called before the first frame update

    private void Awake()
    {
        if (!_dataContainer.IsInitalised)
        {
            _dataContainer.FillDictionary();
        }
        _rotationManager = GetComponent<RotationManager>();
    }

    private void Start()
    {
        Mathf.Clamp(_shopPointer, 0, 3);
        _isRotating = false;
    }

    private void Update()
    {
        CheckForPurchase();
        if (Input.GetKeyDown(KeyCode.K))
        {
            _swipeLeft = false;
            RotateShop(_rotationTime, _swipeLeft);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            _swipeLeft = true;
            RotateShop(_rotationTime, _swipeLeft);
        }
    }
    

    private string ReturnShopColour()
    {
        RaycastHit hit;
        if(Physics.Raycast(_rayOrigin.transform.position,_rayOrigin.transform.TransformDirection(Vector3.forward)
        ,out hit,Mathf.Infinity))
        {
            if (hit.collider.tag == "ShopPlayer")
            {
                return hit.collider.GetComponent<ShopPlayer>().PlayerColour;
            }
        }

        return null;

    }
    public void RotateShop(float rotationTime, bool rotateLeft)
    {
        
        if (rotateLeft)
        {
            var toRotate = transform.localRotation;
            _targetRotation = toRotate * Quaternion.Euler(0, 90, 0);
        }

        if (!rotateLeft)
        {
            var toRotate = transform.localRotation;
            _targetRotation = toRotate * Quaternion.Euler(0, -90, 0);
        }
        
        StartCoroutine(LerpColour(_shopPlayers[_shopPointer].Colour));
        StartCoroutine(_rotationManager.RotateGameObject(_targetRotation, 
            gameObject.transform, rotationTime));
    }
    
    public void PurchaseSkin()
    {
        _dataContainer.UnlockSkin(ReturnShopColour());
        
    }

    private void CheckForPurchase()
    {
        if (_dataContainer.IsSkinBought(ReturnShopColour()))
        {
            _purchasedText.SetText("Set Active");
            _purchaseButton.GetComponent<Button>().onClick.AddListener(SetSkin);
        }
        if (!_dataContainer.IsSkinBought(ReturnShopColour()))
        {
            _purchasedText.SetText("Purchase");
        }
    }

    private void SetSkin()
    {
        _dataContainer.ReturnSkin(ReturnShopColour());
    }
    
    private IEnumerator LerpColour(Color shopTargetColour )
    {
        var m_rotateTime = _rotationTime;
        float timer = 0;
        
        var shopStartColor = _shopLight.color;
        while (timer < m_rotateTime)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / m_rotateTime, 1);
            _shopLight.color = Color.Lerp(shopStartColor, shopTargetColour, percentage);
            yield return null;
        }

        _isRotating = false;
    }
    
  
    
}
