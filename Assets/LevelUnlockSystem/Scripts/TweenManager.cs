using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenManager : MonoBehaviour
{
    public Transform gameLogo;
    public float animDuration;
    public Ease animEase;

    public Transform playButton, moreGameButton, shareButton, rateButton, settingButton;

    public static TweenManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != null)
        {
            Destroy(gameObject);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameLogo.DOMoveY(0.65f, animDuration)
            .SetDelay(0.5f)
            .SetEase(animEase)
            .SetLoops(-1,LoopType.Yoyo);


        playButton.DOScale(1.25f, animDuration)
            .SetDelay(1.5f)
            .SetEase(Ease.InCirc)
            .SetLoops(-1, LoopType.Yoyo);

        moreGameButton.DOScale(1.10f, animDuration)
            .SetDelay(2.0f)
            .SetLoops(-1, LoopType.Yoyo);

        shareButton.DOScale(1.20f, animDuration)
            .SetDelay(0.75f)
            .SetLoops(-1, LoopType.Yoyo);

        rateButton.DOScale(1.20f, animDuration)
            .SetDelay(1.25f)
            .SetLoops(-1, LoopType.Yoyo);

        settingButton.DOScale(1.20f, animDuration)
            .SetDelay(1.5f)
            .SetLoops(-1, LoopType.Yoyo);

    }

   

    public void OpenPanelAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform;

        var temp = transform.localScale;
        temp.y = 0.25f;
        temp.x = 0.25f;
        transform.localScale = temp;

        gameObject.SetActive(true);


        transform.DOScale(1f, 1f);

        
        
    }

    public void ClosePanelAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform;

        transform.DOScale(0.25f, 1f)
            
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                var temp = transform.localScale;
                temp.y = 1f;
                temp.x = 1f;
                transform.localScale = temp;
            });
    }

    public void HintOpenPanelAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform;

        var temp = transform.localScale;
        temp.y = 0.25f;
        temp.x = 0.25f;
        transform.localScale = temp;

        gameObject.SetActive(true);


        transform.DOScale(1f, 1f)
        .OnComplete(() =>
        {
            Time.timeScale = 0f;

        });



    }

    public void HintClosePanelAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform;

        transform.DOScale(0.25f, 1f)

            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                var temp = transform.localScale;
                temp.y = 1f;
                temp.x = 1f;
                transform.localScale = temp;
                Time.timeScale = 1f;
            });
    }


    public void gameObjectPop(GameObject gameObject)
    {
        Transform transform = gameObject.transform;

        transform.DOScale(1.25f,0.5f).
            OnComplete(() =>
        {
            transform.DOScale(0.25f,0.5f).
                OnComplete(()=>
            {
                gameObject.SetActive(false);
            });
        });

    }



    public void gameObjectScale(GameObject gameObject)
    {
        Transform transform = gameObject.transform;

        Vector3 originalScale = gameObject.transform.localScale;


        transform.DOScale(originalScale * 1.35f, animDuration)
            .SetDelay(1.5f)
            .SetLoops(-1, LoopType.Yoyo);

        /* transform.DOScale(originalScale * 1.25f, 0.5f).
             OnComplete(() =>
             {
                 transform.DOScale(originalScale, 0.5f);

             });*/

    }

    public void gameObjectSpin(GameObject gameObject)
    {


        Transform transform = gameObject.transform;

        //Vector3 rotation = new Vector3(0,0,360);
        //transform.DORotate(rotation, 5f, RotateMode.Fast).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);

        transform.DOScale(0.22f, animDuration)
            .SetDelay(0.10f)
            .SetEase(Ease.InCirc)
            .SetLoops(-1, LoopType.Yoyo);

    }
}
