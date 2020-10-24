using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationDict;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        weaponAnimationDict = new Dictionary<Equipment, AnimationClip[]>();
        foreach(WeaponAnimations a in weaponAnimations)
        {
            weaponAnimationDict.Add(a.weapon, a.clips);
        }
    }

    /*void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);
            if (weaponAnimationDict.ContainsKey(newItem))
            {
                currentAttackAnimSet = weaponAnimationDict[newItem];
            }
        }
        else if (newItem != null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 0);
            currentAttackAnimSet = defaultAttackAnimSet;
        }
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
            animator.SetLayerWeight(2, 1);
        else if (newItem != null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield)
            animator.SetLayerWeight(2, 0);
    }*/

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        /* if (Input.GetKeyDown(KeyCode.Q))
         {
             animator.SetTrigger("attackHeavy");
         }
         if (Input.GetKeyDown(KeyCode.E))
         {
             animator.SetTrigger("attackQuick");
         }
         if (Input.GetKeyDown(KeyCode.F))
         {
             animator.SetTrigger("defend");
         }*/


        if (Input.GetButtonUp("Sprint") && PlayerManager.instance.player.GetComponent<PlayerStats>().dash > 0)
        {
            agent.velocity = new Vector3(agent.velocity.x * 10, agent.velocity.y, agent.velocity.z * 10);
            PlayerManager.instance.player.GetComponent<PlayerStats>().dash -= 1;
            PlayerManager.instance.player.GetComponent<PlayerStats>().refillDash();
        }
        if (Input.GetButtonDown("Jump"))
        {
           // animator.SetTrigger("jump");
            agent.velocity = new Vector3(agent.velocity.x, agent.velocity.y + 100, agent.velocity.z);

            // to play from frame x without having to worry about layers 
            animator.Play("Jump", 0, 0.5f);
        }
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
