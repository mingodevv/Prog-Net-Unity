using UnityEngine;

public class PlayMenuController : MonoBehaviour
{
    public GameObject playSubMenu;

    void Start()
    {
        if (playSubMenu != null)
            playSubMenu.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (playSubMenu == null) return;

        bool nextState = !playSubMenu.activeSelf;
        playSubMenu.SetActive(nextState);

        Debug.Log("PlaySubMenu = " + nextState);
    }
}