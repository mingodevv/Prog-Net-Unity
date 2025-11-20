using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGameEnd : MonoBehaviour
{
    private bool _isRestarting;

    public bool IsRestarting
    {
        get => _isRestarting;
        set => _isRestarting = value;
    }
}