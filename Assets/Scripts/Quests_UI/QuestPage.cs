using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPage : MonoBehaviour
{
    #region Singelton

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than once instances of inventory were found!");
        }
        instance = this;
    }

    #endregion

    public static QuestPage instance;


}
