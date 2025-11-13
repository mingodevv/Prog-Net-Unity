using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrbHoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orbCount;
    
    public void OnUpdateOrbCount(int value)
    {
        orbCount.text = value.ToString();
        //Si valeur est 0, chache le UI
        enabled = (value > 0);
    }
}
