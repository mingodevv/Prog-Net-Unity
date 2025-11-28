using UnityEngine;

namespace UI.Ingame
{
    public abstract class UIModel : MonoBehaviour
    {
        public abstract void UpdateUI(bool cond);
        public abstract void TimerSet(float timer); 
    }
}
