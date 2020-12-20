using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour
{

    private GameObject currentScene;
    [SerializeField] private DataScriptableObject _data;
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject achievement;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI shopCurrency;
    
   public void PlayGame()
   {
       SceneManager.LoadScene(1);
   }

   private void Awake()
   {
        SetTutorialText();
        SetCurrencyText();
   }

   private void SetCurrencyText()
   {
       shopCurrency.SetText("Rubix : " + _data.ReturnCoinCount());
   }
   
   private void SetTutorialText()
   {
       tutorialText.SetText(_data.tutorialMode.ToString());
   }

   public void ToggleTutorial()
   {
       _data.ToggleTutorialMode();
       tutorialText.SetText(_data.tutorialMode.ToString());
   }
   
    public void LoadOptionsMenu()
    {
        currentScene = option;
        option.SetActive(true);
        main.SetActive(false);
        
    }

    public void LoadStore()
    {
        currentScene = shop;
        main.SetActive(false);
        shop.SetActive(true);
    }
    
    public void LoadAchievement()
    {
        currentScene = achievement; 
        main.SetActive(false);   
        achievement.SetActive(true);
    }

    public void ReturnToMain()
    {
        currentScene.SetActive(false);
        main.SetActive(true);
    }
    
    public void ExitApplication()
    {
        Application.Quit();
    }
}
