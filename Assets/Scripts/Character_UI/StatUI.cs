using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class StatUI : MonoBehaviour
{
    PlayerStats playerStats;
    public Transform target;

    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.instance.onStatChanged += OnStatChanged;
        playerStats = PlayerStats.instance;
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("CharacterPage"))
        {
            foreach (Text t in GetComponentsInChildren<Text>())
            {
                if (t.name == "StatHealth")
                {
                    t.text = playerStats.currentHealth + " / " + playerStats.maxHealth;
                    break;
                }
                if (t.name == "StatExp")
                {
                    t.text = playerStats.getExp() + " / " + playerStats.maxExp;
                    break;
                }
                if (t.name == "StatDamage")
                {
                    t.text = "" + playerStats.Damage.getValue();
                    break;
                }
                if (t.name == "StatArmor")
                {
                    t.text = "" + playerStats.Armor.getValue();
                    break;
                }
                if (t.name == "StatLevel")
                {
                    t.text = "" + playerStats.level;
                    break;
                }
            }
        }

        if (playerStats.getStatPoint() <= 0 && button != null)
        {
            button.GetComponent<Image>().enabled = false;
            button.interactable = false;
        }
        else if (playerStats.getStatPoint() > 0 && button != null)
        {
            button.GetComponent<Image>().enabled = true;
            button.interactable = true;
        }
    }

    public void addDamage()
    {
        playerStats.addDamage();
    }

    public void addArmor()
    {
        playerStats.addArmor();
    }

    void OnStatChanged(int MaxHealth, int CurrentHealth, int MaxExp, int CurrentExp, int Damage, int Armor, int Level)
    {
        foreach (Text t in GetComponentsInChildren<Text>())
        {
            if (t.name == "StatHealth")
            {
                t.text = CurrentHealth + " / " + MaxHealth;
                break;
            }
            if (t.name == "StatExp")
            {
                t.text = CurrentExp + " / " + MaxExp;
                break;
            }
            if (t.name == "StatDamage")
            {
                t.text = "" + Damage;
                break;
            }
            if (t.name == "StatArmor")
            {
                t.text = "" + Armor;
                break;
            }
            if (t.name == "StatLevel")
            {
                t.text = "" + Level;
                break;
            }
        }
    }
}
