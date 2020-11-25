
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private ObjectSpawner toPause;
    private ScoreSystem scoreSystem;
    
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject gameCanvas;
    
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] private Slider multiplierSlider;
    private void Awake()
    {
        var toRef = GameObject.FindGameObjectWithTag("GameManager");
        scoreSystem = toRef.GetComponent<ScoreSystem>();
        toPause = toRef.GetComponent<ObjectSpawner>();
    }

    private void Update()
    {
        SetScoreText(scoreSystem.Score.ToString());
        SetSlider();
    }
    public void SetScoreText(string score)
    {
        scoreText.SetText(scoreSystem.GetScore());
    }

    public void SetSlider()
    {
        var value = float.Parse(scoreSystem.GetScoreMultiplier()) ;
        multiplierSlider.value = value / 2;
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        toPause.StopMoving();
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        toPause.StartMoving();
    }
    
    public void ResetFunction()
    {
        deathCanvas.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
