using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStats : CharacterStats
{
    //init singelton class
    #region Singelton

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("more than once instances of PlayerStats were found!");
        }
        instance = this;
    }

    #endregion
    public static PlayerStats instance;

    //delegate vars
    public delegate void OnStatChanged(int MaxHealth, int CurrentHealth, int MaxExp, int CurrentExp, int Damage, int Armor, int Level); // whenever we change stats notify everyone else
    public OnStatChanged onStatChanged;

    public delegate void OnTalentChanged(int TalentPoints); // whenever we change talentPoints notify everyone else
    public OnTalentChanged onTalentChanged;

    //totem vars
    bool totemInteraction = false;
    float totemTimePassed;
    Totem totem;

    //player vars
    public int level = 1;
    public int maxExp = 100;
    int currentExp = 0;
    public float autoGenerateLife = 5.0f;

    private int statPoints = 0;
    private int talentPoints = 0;

    //dash vars
    public int dash = 3;
    float lastDash;
    public float DashTimeout = 7;

    //talent vars
    bool expTalent = false;
    bool itemTalent = false;
    bool healthTalent = false;

    //references
    PlayerAnimator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<PlayerAnimator>();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        Invoke("GenerateLife", 0);
    }

    void Update()
    {
        if (totem != null) {
            if (Time.time - totemTimePassed > totem.effectTime)
            {
                switch (totem.totemType)
                {
                    case (TotemType.Fire):
                        {
                            Damage.RemoveModifier(totem.modifier);
                            break;
                        }
                    case (TotemType.Water):
                        {
                            NavMeshAgent agent = GetComponent<NavMeshAgent>();
                            agent.speed -= totem.modifier;
                            break;
                        }
                    case (TotemType.Earth):
                        {
                            Armor.RemoveModifier(totem.modifier);
                            break;
                        }
                    case (TotemType.Air):
                        {
                            // add some skill
                            break;
                        }
                }
                totemInteraction = false;
                totem = null;
                if (onStatChanged != null)
                {
                    onStatChanged.Invoke(maxHealth, currentHealth, maxExp, currentExp, Damage.getValue(), Armor.getValue(), level);
                }
            }
        }

        if(Time.time - lastDash >= DashTimeout)
        {
            dash++;
            dash = Mathf.Clamp(dash, 0, 3);
            lastDash = Time.time;
        }
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null)
        {
            Armor.AddModiefier(newItem.armorModifier);
            Damage.AddModiefier(newItem.damageModifier);
        }
        if (oldItem != null)
        {
            Armor.RemoveModifier(oldItem.armorModifier);
            Damage.RemoveModifier(oldItem.damageModifier);
        }
    }// not being used - in case theres equipment use this

    public override void Die()
    {
        base.Die();
        animator.Die(this, true);
    }//in case player dies

    public void UseHealthPotion(Potion potion)
    {
        int potionModifier = potion.modifier;
        if (itemTalent)
            potionModifier *= 2;
        currentHealth += potionModifier;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (onStatChanged != null)
        {
            onStatChanged.Invoke(maxHealth, currentHealth, maxExp, currentExp, Damage.getValue(), Armor.getValue(), level);
        }

    }

    public void UseExpPotion(Potion potion)
    {
        int potionModifier = potion.modifier;
        if (itemTalent)
            potionModifier *= 2;
        currentExp += potionModifier;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
        if (onStatChanged != null)
        {
            onStatChanged.Invoke(maxHealth, currentHealth, maxExp, currentExp, Damage.getValue(), Armor.getValue(), level);
        }
    }

    public void UseScroll(Scroll scroll, int scrollSlot)//using a scroll based on her type and modify value
    {
        int scrollModifier = scroll.modifier;
        if (itemTalent)
            scrollModifier *= 2;
        switch (scrollSlot)
        {
            case 0://talent scroll
                {
                    talentPoints += scrollModifier;
                    if (onTalentChanged != null)
                        onTalentChanged.Invoke(talentPoints);
                    break;
                }
            case 1://exp scroll
                {
                    addExp(scrollModifier, "Scroll");
                    break;
                }
            case 2://damage scroll
                {
                    Damage.AddModiefier(scrollModifier);
                    break;
                }
            case 3://armor scroll
                {
                    Armor.AddModiefier(scrollModifier);
                    break;
                }
            case 4://health scroll
                {
                    maxHealth += scrollModifier;
                    break;
                }
        }
        if (onStatChanged != null)
        {
            onStatChanged.Invoke(maxHealth, currentHealth, maxExp, currentExp, Damage.getValue(), Armor.getValue(), level);
        }
    }

    public void LevelUp()
    {
        level += 1;
        currentExp = (currentExp % maxExp);
        maxExp *= 2;
        statPoints += 2;
        talentPoints += 1;
        if (onTalentChanged != null)
            onTalentChanged.Invoke(talentPoints);
        Debug.Log("Leveled up!");
        if (onStatChanged != null)
        {
            onStatChanged.Invoke(maxHealth, currentHealth, maxExp, currentExp, Damage.getValue(), Armor.getValue(), level);
        }
    }
    
    new public int getExp()
    {
        return currentExp;
    }//return exp

    public void addExp(int exp, string source)//adds exp, get amount and param which tells how u got exp
    {
        if (expTalent)
        {
            switch (source)
            {
                case ("Enemy"):
                    {
                        currentExp += (exp * 2);
                        break;
                    }
                default:
                    {
                        currentExp += exp;
                        break;
                    }
            }
        }
        else
            currentExp += exp;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
        if (onStatChanged != null)
        {
            onStatChanged.Invoke(maxHealth, currentHealth, maxExp, currentExp, Damage.getValue(), Armor.getValue(), level);
        }
    }

    public void interctWithTotem(Totem newTotem, TotemType totemType)
    {
        Debug.Log("Interacting with " + totemType + " Totem!");
        switch (totemType)
        {
            case TotemType.Fire:
                {
                    if(totemInteraction)
                    {
                        Debug.Log("already have interacted with another totem, unequip it or wait for side effect to desolve");
                        break;
                    }
                    totem = newTotem;
                    Damage.AddModiefier(totem.modifier);
                    totemInteraction = true;
                    totemTimePassed = Time.time;
                    break;
                }
            case TotemType.Earth:
                {
                    if (totemInteraction)
                    {
                        Debug.Log("already have interacted with another totem, unequip it or wait for side effect to desolve");
                        break;
                    }
                    totem = newTotem;
                    Armor.AddModiefier(totem.modifier);
                    totemInteraction = true;
                    totemTimePassed = Time.time;
                    break;
                }
            case TotemType.Water:
                {
                    if (totemInteraction)
                    {
                        Debug.Log("already have interacted with another totem, unequip it or wait for side effect to desolve");
                        break;
                    }
                    totem = newTotem;
                    NavMeshAgent agent = GetComponent<NavMeshAgent>();
                    agent.speed += totem.modifier;
                    totemInteraction = true;
                    totemTimePassed = Time.time;
                    break;
                }
            case TotemType.Air:
                {
                    if (totemInteraction)
                    {
                        Debug.Log("already have interacted with another totem, unequip it or wait for side effect to desolve");
                        break;
                    }
                    //add some skill
                    totem = newTotem;
                    totemInteraction = true;
                    totemTimePassed = Time.time;
                    break;
                }
        }
        if (onStatChanged != null)
        {
            onStatChanged.Invoke(maxHealth, currentHealth, maxExp, currentExp, Damage.getValue(), Armor.getValue(), level);
        }
    }

    public void refillDash()
    {
        if(dash == 3)
            lastDash = Time.time;
    }

    public void GenerateLife()
    {
        if(currentHealth < maxHealth)
        {
            currentHealth++;
            if (onStatChanged != null)
            {
                onStatChanged.Invoke(maxHealth, currentHealth, maxExp, currentExp, Damage.getValue(), Armor.getValue(), level);
            }
        }
        Invoke("GenerateLife", autoGenerateLife);
    }


    public void addDamage() 
    {
        if (statPoints <= 0)
            return;
        Damage.addToBase();
        statPoints--;
        if (onStatChanged != null)
        {
            onStatChanged.Invoke(maxHealth, currentHealth, maxExp, currentExp, Damage.getValue(), Armor.getValue(), level);
        }
    }

    public void addArmor() 
    {
        if (statPoints <= 0)
            return;
        Armor.addToBase();
        statPoints--;
        if (onStatChanged != null)
        {
            onStatChanged.Invoke(maxHealth, currentHealth, maxExp, currentExp, Damage.getValue(), Armor.getValue(), level);
        }
    }

    public int getStatPoint()
    {
        return statPoints;
    }

    public int getTalentPoint()
    {
        return talentPoints;
    }

    public void ActivateTalent(TalentStyle talentStyle, Talent talent)
    {
        switch (talentStyle)
        {
            case (TalentStyle.ExpModifier):
            {
                    expTalent = true;
                    break;
            }
            case (TalentStyle.HealthModifier):
                {
                    healthTalent = true;
                    autoGenerateLife = autoGenerateLife / talent.modifier;
                    break;
                }
            case (TalentStyle.ItemModifier):
                {
                    itemTalent = true;
                    break;
                }
        }
    }
}
