using UnityEngine;

public class CharacterSelectMenu : MonoBehaviour
{
    public GameObject panel;

    public void OpenMenu()
    {
        panel.SetActive(true);
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
    }

    public void SelectClass(string className)
    {
        Debug.Log("Classe sélectionnée : " + className);
        CloseMenu();
    }
}