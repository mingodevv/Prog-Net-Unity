using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class HeroChoice : NetworkBehaviour
{
    
    private NetworkVariable<FixedString64Bytes> selectedClass = new();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
            selectedClass.Value = ClassManager.Instance.GetSelectedClass();
    }

    public override void OnGainedOwnership()
    {
        base.OnGainedOwnership();
        if (IsOwner)
            selectedClass.Value = ClassManager.Instance.GetSelectedClass();
    }

    public string SelectedClass
    {
        get => selectedClass.Value.ToString();
        set => selectedClass.Value = value;
    }
    private void Start()
    {
        selectedClass.OnValueChanged += HandleSelectedClassChange;
    }

    private void HandleSelectedClassChange(FixedString64Bytes previousValue, FixedString64Bytes newValue)
    {
        selectedClass.Value = newValue;
    }
}
