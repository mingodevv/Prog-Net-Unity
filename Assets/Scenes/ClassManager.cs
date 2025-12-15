using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    
    #region Singleton
    public static ClassManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion
    
    private string selectedClass;

    public string SelectedClass
    {
        set => selectedClass = value;
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public string GetSelectedClass()
    {
        return selectedClass;
    }
}
