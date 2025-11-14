using UnityEngine;

public class CharacterSelectMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject characterSelectPanel;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        characterSelectPanel.SetActive(false);
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
        
        CloseMenu();
    }
}