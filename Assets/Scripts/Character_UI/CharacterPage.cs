using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPage : MonoBehaviour
{
    #region Singelton

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than once instances of Character page were found!");
        }
        instance = this;
    }

    #endregion

    public static CharacterPage instance;

    public delegate void OnStatChanged();
    public OnStatChanged onStatChangedCallback;
    public List<Item> items = new List<Item>();

}
