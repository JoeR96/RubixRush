using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private DataScriptableObject _data;
    [SerializeField] private GameObject[] main;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject[] shop;
    [SerializeField] private GameObject achievement;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI shopCurrency;
    
    [SerializeField] private string currentScene;
    
   public void PlayGame()
   {
       SceneManager.LoadScene(1);
   }

   private void Awake()
   {
        SetTutorialText();
        SetCurrencyText();
   }

   private void Start()
   {
       currentScene = "main";
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
   
   public void ReturnToMainMenu()
   {
       AdManager.Instance.PlayAd();
       currentScene = "main";
       foreach (var main in main)
       {
           main.SetActive(true);
       }
   }
    
   private void CloseMainMenu( )
   {
       {
           foreach (var main in main)
           {
               main.SetActive(false);
           }
       }

   }
   
   public void LoadStoreMenu()
   {
       CloseMainMenu();
       foreach (var t in shop)
       {
           t.SetActive(true);
       }
   }
   
   public void CloseStoreMenu()
   {
       foreach (var t in shop)
       { 
           t.SetActive(false);
       }

       ReturnToMainMenu();
   }
   
    public void LoadOptionsMenu()
    {
        CloseMainMenu();
        option.SetActive(true);

    }

    public void CloseOptionsMenu()
    {
        option.SetActive(false);
        ReturnToMainMenu();
    }
    
    public void LoadAchievementMenu()
    {
        CloseMainMenu();
        achievement.SetActive(true);
    }

    public void CloseAchievementMenu()
    {
        achievement.SetActive(false);
        ReturnToMainMenu();
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

}
