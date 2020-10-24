using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; protected set; }

    public Stat Damage;
    public Stat Armor;

    public int getExp()
    {
        int Exp = Mathf.RoundToInt((Damage.getValue() + Armor.getValue()) * 3.5f);
        return Exp;
    }

    public event System.Action<int, int> OnHealthChanged;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
           // takeDamage(10);
        }
    }

    public void takeDamage(int damage)
    {
        damage -= Armor.getValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        //Debug.Log(transform.name + " take " + damage + " damage.");

        if(OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //die in some way.
    }


}
