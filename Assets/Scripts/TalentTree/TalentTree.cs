using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentTree : MonoBehaviour
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

    public static TalentTree instance;

    public int usedTalentPoints;
    public int currentTalentPoints;

    public TalentUI talentUI;


    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.instance.onTalentChanged += OnTalentChanged;
        currentTalentPoints = PlayerStats.instance.getTalentPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTalentChanged(int TalentPoints)
    {
        currentTalentPoints = TalentPoints - usedTalentPoints;
        talentUI.UpdateUI();
    }
}
