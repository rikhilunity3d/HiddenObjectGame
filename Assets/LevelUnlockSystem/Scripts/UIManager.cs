using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LevelUnlockSystem
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;



        [SerializeField] private Text timerText;
        [SerializeField] private GameObject hiddenObjectsIconsHolder;
        [SerializeField] private GameObject hiddenObjectIconPrefab;

        private List<GameObject> hiddenObjectsIconsList;

        [SerializeField]
        private GameObject hintPanel;

        public GameObject HintPanel { get { return hintPanel; } }

        public Text TimerText { get { return timerText; } }

        // Game Over Panel

        [SerializeField] private Image[] starsArray;            //array of stars
        [SerializeField] private Text levelStatusText;          //level status text
        [SerializeField] private GameObject overPanel;          //ref to over panel
        [SerializeField] private Color lockColor, unlockColor;  //ref to colors

        public GameObject OverPanel { get { return overPanel; } }




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

            hiddenObjectsIconsList = new List<GameObject>();
        }

        public void PopulateHiddenObjectIcon(List<HiddenObjectData> activeHiddenObjectList)
        {
            hiddenObjectsIconsList.Clear();

            for (int i = 0; i < activeHiddenObjectList.Count; i++)
            {
                GameObject icon = Instantiate(hiddenObjectIconPrefab, hiddenObjectsIconsHolder.transform);
                icon.name = activeHiddenObjectList[i].hiddenObject.name;
                Image childImage = icon.transform.GetChild(0).GetComponent<Image>();
                Text childText = icon.transform.GetChild(1).GetComponent<Text>();

                childImage.sprite = activeHiddenObjectList[i].hiddenObject.GetComponent<SpriteRenderer>().sprite;

                childText.text = activeHiddenObjectList[i].hiddenObject.name;

                hiddenObjectsIconsList.Add(icon);
            }
        }

        public void CheckSelectedHiddenObject(string objectName)
        {
            for (int i = 0; i < hiddenObjectsIconsList.Count; i++)
            {
                if (hiddenObjectsIconsList[i].name == objectName)
                {
                    hiddenObjectsIconsList[i].SetActive(false);
                    break;
                }
            }
        }

        public void HintButton()
        {
            GameObject[] hint = GameObject.FindGameObjectsWithTag("Hint");
            for (int i = 0; i < hint.Length; i++)
            {
                Destroy(hint[i].gameObject);
            }

            if (SFXManager.instance.soundToogle == true)
            {
                SFXManager.instance.audioSource.PlayOneShot(SFXManager.instance.Click);
            }

            if (LevelManager.instance.hint >= 1)
            {
                LevelManager.instance.HintMethod();
                LevelManager.instance.hint--;
            }
            else
            {
                HintPanelOpenOrClose();
            }

        }

        public void HintPanelOpenOrClose()
        {
            if (hintPanel.activeSelf == false)
            {
                TweenManager.instance.HintOpenPanelAnimation(hintPanel);
                LevelManager.instance.HiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].gameObject.SetActive(false);
            }
            else
            {
                TweenManager.instance.HintClosePanelAnimation(hintPanel);
                Time.timeScale = 1f;
                LevelManager.instance.HiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].gameObject.SetActive(true);

                GameObject.Find("Button").GetComponent<Button>().interactable = true;

                GameObject.Find("Button").GetComponent<Button>().GetComponentInChildren<Text>().text = "Get a Hint";

            }
        }
        public void GetHintButton()
        {
            HintPanelOpenOrClose();

            Time.timeScale = 0f;

           
                if (LevelManager.instance.hint <= 0)
                {
                    LevelManager.instance.hint++;


                Time.timeScale = 1f;


            }



        }



        


        public void NextButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GameOver(int starCount)                     //method called by the buttons in the scene
        {
            StartCoroutine(Waiter(10f));



            if (starCount > 0)                                  //if start count is more than 0
            {                                                   //set the levelStatusText
                levelStatusText.text = "Level " + (LevelSystemManager.Instance.CurrentLevel + 1) + " Complete";
                LevelSystemManager.Instance.LevelComplete(starCount);   //send the information to LevelSystemManager

                if (SFXManager.instance.soundToogle == true)
                {
                    SFXManager.instance.audioSource.PlayOneShot(SFXManager.instance.Complete);
                }
            }
            else
            {
                //else only set the levelStatusText
                levelStatusText.text = "Level " + (LevelSystemManager.Instance.CurrentLevel + 1) + " Failed";
                if (SFXManager.instance.soundToogle == true)
                {
                    SFXManager.instance.audioSource.PlayOneShot(SFXManager.instance.Fail);
                }
            }
            SetStar(starCount);                                 //set the stars
            //overPanel.SetActive(true);                          //activate the over panel
            TweenManager.instance.OpenPanelAnimation(overPanel);
        }

        public void OkBtn()                                     //method called by ok button
        {
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// Method to show number of stars achieved by the player for this particular level
        /// </summary>
        /// <param name="starAchieved"></param>
        private void SetStar(int starAchieved)
        {
            for (int i = 0; i < starsArray.Length; i++)             //loop through entire star array
            {
                /// <summary>
                /// if i is less than starAchieved
                /// Eg: if 2 stars are achieved we set the start at index 0 and 1 color to unlockColor, as array start from 0 element
                /// </summary>
                if (i < starAchieved)
                {
                    starsArray[i].color = unlockColor;              //set its color to unlockColor
                }
                else
                {
                    starsArray[i].color = lockColor;                //else set its color to lockColor
                }
            }
        }



        IEnumerator Waiter(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

    }



}
