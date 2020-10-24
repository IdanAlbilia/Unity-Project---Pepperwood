using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent : MonoBehaviour
{
    public int talentCost;
    new public string name = "new talent";
    public string description;

    public bool activated = false;

    public int modifier = 0;

    public TalentStyle talentStyle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateTalent()
    {
        activated = true;
        PlayerStats.instance.ActivateTalent(talentStyle, this);
    }
}

public enum TalentStyle { ExpModifier, CritModifier, DodgeModifier, ItemModifier, HealthModifier }
