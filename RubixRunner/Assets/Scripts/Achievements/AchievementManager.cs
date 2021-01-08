using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AchievementManager : MonoBehaviour
{
    [SerializeField] protected DataScriptableObject _data;
    [SerializeField] protected Slider progress;
    [SerializeField] protected TextMeshProUGUI progressText;
    protected float target;
    protected float value;

    private void Start()
    {
        SetProgressSlider();
    }
    protected virtual void SetProgressSlider()
    {
        progress.value = value / target;
    }

    protected virtual void SetProgressText()
    {
        progressText.SetText(value.ToString());
    }
    
}
