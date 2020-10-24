using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scroll", menuName = "Inventory/Scroll")]
public class Scroll : Item
{
    PlayerStats playerStats;
    public ScrollSlot scrollSlot;

    public int modifier;

    public MeshRenderer mesh;

    public override void Use()
    {
        base.Use();
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        playerStats.UseScroll(this, (int)scrollSlot);
        RemoveFromInventory();
    }
}
public enum ScrollSlot { Talent, Exp, damageModifier, armorModifier, healthModifier }
