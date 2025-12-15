using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject characterSelectPanel;

    [Header("UI Previews (RawImages)")]
    public RawImage tankPreview;
    public RawImage assassinPreview;
    public RawImage sniperPreview;

    private string currentClass = "Tank";

    void Start()
    {
        mainMenuPanel.SetActive(true);
        characterSelectPanel.SetActive(false);

        ApplyClass(currentClass);
    }

    public void OpenMenu()
    {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        characterSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void SelectClass(string className)
    {
        Debug.Log("Classe sélectionnée : " + className);

        currentClass = className;
        ApplyClass(currentClass);

        CloseMenu();
        ClassManager.Instance.SelectedClass = currentClass;
    }

    void ApplyClass(string className)
    {
        tankPreview.gameObject.SetActive(false);
        assassinPreview.gameObject.SetActive(false);
        sniperPreview.gameObject.SetActive(false);

       
        switch (className)
        {
            case "Tank":
                tankPreview.gameObject.SetActive(true);
                break;

            case "Assassin":
                assassinPreview.gameObject.SetActive(true);
                break;

            case "Sniper":
                sniperPreview.gameObject.SetActive(true);
                break;
        }
    }
}