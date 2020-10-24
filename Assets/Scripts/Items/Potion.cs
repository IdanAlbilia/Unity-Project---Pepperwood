using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    PlayerStats playerStats;
    public PotionSlot potionSlot;

    public int modifier;

    public MeshRenderer mesh;

    public override void Use()
    {
        base.Use();
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        if ((int)potionSlot == 0)
        {
            playerStats.UseHealthPotion(this);
            RemoveFromInventory();
        }
        if ((int)potionSlot == 1)
        {
            playerStats.UseExpPotion(this);
            RemoveFromInventory();
        }
    }
}
public enum PotionSlot { Health, Exp }

