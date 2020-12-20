using UnityEngine;
using UnityEngine.UI;

public abstract class AchievementManager : MonoBehaviour
{
    [SerializeField] protected DataScriptableObject _data;
    [SerializeField] protected Slider progress;
    protected float target;
    protected float value;

    private void Start()
    {
        SetProgressSlider();
    }
    protected virtual void SetProgressSlider()
    {
        progress.value = value / target * 100;
    }
    
}
