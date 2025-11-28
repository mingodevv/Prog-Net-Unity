using UnityEngine;

public class ChooseButton : MonoBehaviour
{
    public string className;
    public CharacterSelectMenu menu;

    public void OnChoose()
    {
        menu.SelectClass(className);
        Debug.Log("Classe choisie : " + className);
    }
}