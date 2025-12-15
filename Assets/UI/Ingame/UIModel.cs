using System;
using Unity.Netcode;
using UnityEngine;

namespace UI.Ingame
{
    public abstract class UIModel : NetworkBehaviour
    {
        private NetworkVariable<bool> isVisible = new();

        public NetworkVariable<bool> IsVisible
        {
            get => isVisible;
            set => isVisible = value;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            isVisible.OnValueChanged += HandleVisibleChangeCallback;
        }

        private void HandleVisibleChangeCallback(bool previousValue, bool newValue)
        {
            UpdateUI(newValue);
        }
        


        public virtual void UpdateUI(bool cond)
        {
            if (IsServer)
                IsVisible.Value = cond;
            gameObject.SetActive(cond);
        }

    }
}