using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private ObjectSpawner toPause;
    private ScoreSystem scoreSystem;
    private Manager _gameManager;
    [Header("Canvas")]
    [SerializeField] private GameObject _gameManagerObject;
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject gameCanvas;
    
    [Header("InGame Variables")]
    [SerializeField] private Button distanceButton;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider multiplierSlider;
    
    [Header("EndGameText")]
    [SerializeField] private TextMeshProUGUI _endScore;
    [SerializeField] private TextMeshProUGUI _endDistance;
    [SerializeField] private TextMeshProUGUI _endPickup;
    private string distance = "45000";

    private float displayDistanceBannerTimer = 0f;
    private float displayTime = 2f;
    private bool distanceBannerIsDisplaying;
    private bool IsLerping;


    private void Awake()
    {
        _gameManager = _gameManagerObject.GetComponent<Manager>();
        distanceBannerIsDisplaying = false;
        var toRef = GameObject.FindGameObjectWithTag("GameManager");
        scoreSystem = toRef.GetComponent<ScoreSystem>();
        toPause = toRef.GetComponent<ObjectSpawner>();
    }

    private void Update()
    {
        SetScoreText(scoreSystem.Score.ToString());
        SetSlider();
        DisplayDistanceBanner();
    }

    public void DisplayDistanceBanner()
    {
        //coroutine in here
        if (distanceBannerIsDisplaying)
        {
            distanceButton.gameObject.SetActive(true);
            SetDistanceText(distance);
            
            if (displayDistanceBannerTimer > 0)
            {
                displayDistanceBannerTimer -= Time.deltaTime;
            }
            
            if (displayDistanceBannerTimer <= 0)
            {
                distanceButton.gameObject.SetActive(false);
                distanceBannerIsDisplaying = false;
            }
        }
    }
    public void SetScoreText(string score)
    {
        scoreText.SetText(scoreSystem.GetScore());
    }

    //overridable
    public void SetSlider()
    {
        var value = scoreSystem.GetScoreMultiplier() ;
        multiplierSlider.value = value;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }
    
    public void ResetFunction()
    {
        deathCanvas.SetActive(false);
        SceneManager.LoadScene(1);
    }

    private void ShowDistance()
    {
        distanceButton.gameObject.SetActive(true);
        //we need to save the distance variable first
        distanceText.SetText("distance");
        distanceButton.gameObject.SetActive(false);
        
    }

    private void SetDistanceText(string distance)
    {
        distanceText.SetText(distance);
    }

    public void SetGameOverText()
    {
        _endScore.SetText("Score: " + scoreSystem.Score);
        _endPickup.SetText("Rubix: " + scoreSystem.Coins);
        _endDistance.SetText("Distance " + _gameManager.DistanceTravelled);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
    
}
