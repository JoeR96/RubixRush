using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicUI : MonoBehaviour
{
    [SerializeField] private GameObject _swipeImage;
    [SerializeField] private GameObject _targetPoint;
    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;
    [SerializeField] private Transform _upTarget;

    public bool Tutorial { get; set; }
    private float _timer;
    private Transform _centre;
    private enum tutorialState { LEFT, RIGHT, UP, DONE };

    private float tutorialTimer;

    private float currentTutorialTimer;
    private float _durationBetweenAnimation = 2f;
    private bool _isIncrementing;

    // Start is called before the first frame update
    void Start()
    {
        _isIncrementing = true;
        _centre = _swipeImage.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Tutorial)
        {
            TutorialTimer();
        }
        
    }

    

    private IEnumerator FadeImage(GameObject image,float target)
    {
        var toFade = image.GetComponent<Image>();
        const float fadeTime = 1.75f;
        var alpha = toFade.color.a;
        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / fadeTime, 1);
            Color newColour = new Color(1,1,1,Mathf.Lerp(alpha,target,percentage));
            toFade.color = newColour;
            yield return null;
        }
    }
    
    public IEnumerator LerpGameObjectPosition(GameObject toLerp, Vector3 target,float rate)
    {
        float time = 2f;
        float i = 0;
        float r = rate / time;
        var start = toLerp.transform.position;
        while (i < 1)
        {
            i += Time.deltaTime * rate;
            toLerp.transform.position = Vector3.Lerp(start, target, i);
            //transform.rotation = Quaternion.RotateTowards(start, target, i);
            yield return null;
        }
        ReturnToCentre();
    }

    private void ReturnToCentre()
    {
        _swipeImage.transform.position = _targetPoint.transform.position;
        _swipeImage.GetComponent<Image>().color = new Color(1,1,1,1);
    }

    private void SwipeLeft()
    {
        StartCoroutine(LerpGameObjectPosition(_swipeImage, _leftTarget.position, 0.5f));
        StartCoroutine(FadeImage(_swipeImage, 0));
    }

    //Reverse the target and start point between the up and down positions
    private IEnumerator SwipeDown()
    {
        _swipeImage.transform.position = _upTarget.transform.position;
        StartCoroutine(LerpGameObjectPosition(_swipeImage, _targetPoint.transform.position, 0.5f));
        StartCoroutine(FadeImage(_swipeImage, 0));
        yield return new WaitForSeconds(0.5f);
        _swipeImage.gameObject.SetActive(false);
    }
    
    private void SwipeUp()
    {
        StartCoroutine(LerpGameObjectPosition(_swipeImage, _upTarget.position, 0.5f));
        StartCoroutine(FadeImage(_swipeImage, 0));
    }
    
    private void SwipeRight()
    {
        StartCoroutine(LerpGameObjectPosition(_swipeImage, _rightTarget.position, 0.5f));
        StartCoroutine(FadeImage(_swipeImage, 0));
    }

    private void TutorialTimer()
    {
        if (_isIncrementing)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
        
            if (GameIncrementer == 4)
            {
                _isIncrementing = false;
                return;
            }
            
            if (!(_timer <= 0)) return;
            GameIncrementer++;
            _timer = _durationBetweenAnimation;
            NextSwipeTutorial(GameIncrementer);
        }
    }

    private void NextSwipeTutorial(int pointer)
    {
        if (pointer == 1)
        {
            SwipeLeft();
        }
        if (pointer == 2)
        {
            SwipeRight();
        }
        if (pointer == 3)
        {
            SwipeUp();
        }
        if (pointer == 4)
        {
            SwipeDown();
        }
    }
    public int GameIncrementer { get; set; }
}
