using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentSlot : MonoBehaviour
{
    public Button activateButton;
    public Image icon;
    Talent talent;

    TalentUI talentUI;

    TalentTree TalentTree;

    Color c;

    public Text hoverText;
    public Image HoverBackground;
    public Image HoverBorder;

    // Start is called before the first frame update
    void Start()
    {
        c = gameObject.GetComponent<Image>().color;
        c.a = 0.2f;

        TalentTree = TalentTree.instance;
        talent = GetComponent<Talent>();
        talentUI = GetComponentInParent<TalentUI>();

        hoverText.enabled = false;
        HoverBorder.enabled = false;
        HoverBackground.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateTalent()
    {
        if(TalentTree.currentTalentPoints >= talent.talentCost && !talent.activated)
        {
            TalentTree.usedTalentPoints += talent.talentCost;
            TalentTree.currentTalentPoints -= talent.talentCost;
            talentUI.UpdateUI();
            talent.activateTalent();
            c.a = 1f;
            gameObject.GetComponent<Image>().color = c;
        }
        else
        {
            Debug.Log("not enough talent points / u already have this talent!");
        }
    }

    public void OnMouseEnter()
    {
        if (talent)
        {
            hoverText.enabled = true;
            HoverBackground.enabled = true;
            HoverBorder.enabled = true;
            hoverText.text = talent.description;
        }
    }

    public void OnMouseExit()
    {
        hoverText.enabled = false;
        HoverBackground.enabled = false;
        HoverBorder.enabled = false;
    }
}
