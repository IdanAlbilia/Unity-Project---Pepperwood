using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    public float attackDelay = .6f;

    CharacterStats myStats;
    CharacterStats opponentStats;

    public event System.Action OnAttack;

    public bool InCombat { get; private set; }
    const float combatCooldown = 5;
    float lastAttackTime;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;   

        if(Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false; 
        }
    }

    public void Attack(CharacterStats targerStats)
    {
        if(attackCooldown <= 0f)
        {
            opponentStats = targerStats;
            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;

        }
    }


    public void AttackHit_AnimationEvent()
    {
        opponentStats.takeDamage(myStats.Damage.getValue());
        if (opponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }
}
