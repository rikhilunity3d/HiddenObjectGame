using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LevelUnlockSystem
{

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        [SerializeField]
        private float timeLimit;

        [SerializeField]
        public int hint = 1;

        [SerializeField]
        private GameObject levelBackgroundImage;

        [SerializeField]
        private string changeBackgroundCaption;

        [SerializeField]
        private List<HiddenObjectHolder> hiddenObjectHolderList;

        public List<HiddenObjectHolder> HiddenObjectHolderList { get { return hiddenObjectHolderList; } }

        private List<HiddenObjectData> activeHiddenObjectList;

        [SerializeField]
        int maxActiveHiddenObjectsCount = 5;

        [SerializeField]
        private GameObject particle;

        public GameObject TouchParticle { get { return particle; } }

        [SerializeField]
        private GameObject hintObject;


        private int totalHiddenObjectCount = 5;
        private float currentTime = 0;

        private GameStatus gameStatus = GameStatus.NEXT;

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

        private void Start()
        {
            activeHiddenObjectList = new List<HiddenObjectData>();
            AssignHiddenObjects();
        }

        void AssignHiddenObjects()
        {


            currentTime = timeLimit;

            UIManager.instance.TimerText.text = "" + currentTime;

            totalHiddenObjectCount = 0;

            activeHiddenObjectList.Clear();

            levelBackgroundImage.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelBackgrounds/Level" + (LevelSystemManager.Instance.CurrentLevel + 1));


            hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].gameObject.SetActive(true);

            for (int i = 0; i < hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].HiddenObjectsList.Count; i++)
            {
                hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].HiddenObjectsList[i].hiddenObject.GetComponent<Collider2D>().enabled = false;
            }

            int j = 0;
            while (true)
            {


                int randomVal = UnityEngine.Random.Range(0, hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].HiddenObjectsList.Count);



                Debug.Log("Active Hidden Object List: " + LevelSystemManager.Instance.CurrentLevel);

                if (!hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].HiddenObjectsList[randomVal].makeHidden)
                {

                    hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].HiddenObjectsList[randomVal].hiddenObject.name = "" + j;

                    hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].HiddenObjectsList[randomVal].makeHidden = true;

                    hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].HiddenObjectsList[randomVal].hiddenObject.GetComponent<Collider2D>().enabled = true;

                    activeHiddenObjectList.Add(hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].HiddenObjectsList[randomVal]);
                }

                if (activeHiddenObjectList.Count == maxActiveHiddenObjectsCount)
                {
                    Debug.Log("activeHiddenObjectList : " + activeHiddenObjectList.Count);
                    break;

                }

                j++;

            }


            for (int i = 0; i < activeHiddenObjectList.Count; i++)
            {
                print(activeHiddenObjectList[i].hiddenObject.gameObject.name);
            }

            UIManager.instance.PopulateHiddenObjectIcon(activeHiddenObjectList);

            gameStatus = GameStatus.PLAYING;
        }


        private void Update()
        {
            if (gameStatus == GameStatus.PLAYING)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.zero);

                    if (hit && hit.collider != null)
                    {
                        Vector2 position;

                        position = hit.collider.gameObject.transform.position;


                        // Instantiate particle
                        GameObject p = Instantiate(particle, position, particle.transform.rotation);

                        Destroy(p, 1f);
                        TweenManager.instance.gameObjectPop(hit.collider.gameObject);
                        // hit.collider.gameObject.SetActive(false);

                        GameObject[] hint = GameObject.FindGameObjectsWithTag("Hint");
                        for (int i = 0; i < hint.Length; i++)
                        {
                            Destroy(hint[i].gameObject);
                        }

                        // Play Music
                        if (SFXManager.instance.soundToogle == true)
                        {
                            SFXManager.instance.audioSource.PlayOneShot(SFXManager.instance.Click);
                        }
                        // End of Play Music 

                        UIManager.instance.CheckSelectedHiddenObject(hit.collider.gameObject.name);
                        Debug.Log("Object Name : " + hit.collider.gameObject.name);


                        for (int i = 0; i < activeHiddenObjectList.Count; i++)
                        {
                            if (activeHiddenObjectList[i].hiddenObject.name == hit.collider.gameObject.name)
                            {
                                activeHiddenObjectList.RemoveAt(i);
                                break;
                            }
                        }

                        totalHiddenObjectCount++;

                        if (totalHiddenObjectCount >= maxActiveHiddenObjectsCount)
                        {
                            Debug.Log("Level Complete");

                            hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].gameObject.SetActive(false);

                            if (currentTime > (timeLimit / 2))
                            {
                                UIManager.instance.GameOver(3);                     //method called by the buttons in the scene
                            }

                            else if (currentTime <= (timeLimit / 2) && currentTime >= ((timeLimit / 2) / 2))
                            {
                                UIManager.instance.GameOver(2);                     //method called by the buttons in the scene
                            }
                            else if (currentTime > 0 && currentTime < ((timeLimit / 2) / 2))
                            {

                                UIManager.instance.GameOver(1);                     //method called by the buttons in the scene
                            }

                            gameStatus = GameStatus.NEXT;

                            // LevelSystemManager.Instance.LevelComplete(3);

                        }
                    }
                }

                currentTime -= Time.deltaTime;
                TimeSpan time = TimeSpan.FromSeconds(currentTime);

                UIManager.instance.TimerText.text = time.ToString("mm':'ss");

                if (currentTime <= 0)
                {
                    currentTime = 0;
                    Debug.Log("Level Lost");
                    hiddenObjectHolderList[LevelSystemManager.Instance.CurrentLevel].gameObject.SetActive(false);

                    UIManager.instance.GameOver(0);                     //method called by the buttons in the scene

                    gameStatus = GameStatus.NEXT;

                }
            }
        }

        public void HintMethod()
        {



            // Play Music
            if (SFXManager.instance.soundToogle == true)
            {
                SFXManager.instance.audioSource.PlayOneShot(SFXManager.instance.Hint);
            }
            // End of Play Music

            int randomVal = UnityEngine.Random.Range(0, activeHiddenObjectList.Count);

            Vector2 position;
            Vector2 scale;

            position = activeHiddenObjectList[randomVal].hiddenObject.transform.position;

            scale = hintObject.transform.localScale;
            scale.x = 2f;
            scale.y = 2f;
            hintObject.transform.localScale = scale;


            Instantiate(hintObject, position, hintObject.transform.rotation);

            TweenManager.instance.gameObjectSpin(hintObject);

            TweenManager.instance.gameObjectScale(activeHiddenObjectList[randomVal].hiddenObject);

        }


    }



}



[System.Serializable]
public class HiddenObjectData
{
    public GameObject hiddenObject;
    public string name;
    public bool makeHidden = false;
}

public enum GameStatus
{
    PLAYING,
    NEXT
}