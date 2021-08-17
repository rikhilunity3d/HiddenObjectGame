using UnityEngine;

public class PanelOpener : MonoBehaviour
{

    [SerializeField]
    private GameObject Panel;
    public void OpenPanel()
    {
        if(Panel != null)
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
    }
}
