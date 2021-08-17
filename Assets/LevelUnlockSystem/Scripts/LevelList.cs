using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelList : MonoBehaviour
{
    [SerializeField]
    private List<Level> levelList = new List<Level>();
    [SerializeField]
    private GameObject Panel;


 
    public void OpenPanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(true);
        }

    }
    public void ClosePanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(false);
        }
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }
}

[System.Serializable]
public class Level
{
    public int levelID;

    public Button levelButton;

    public string levelName;

}
