using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equiptment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armorModifier;
    public int damageModifier;

    public EquipmentMeshRegion[] coveredMeshRegions;

    public SkinnedMeshRenderer mesh;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot {  Head, Chest, Legs, Weapon, Shield, Feet, Ring }
public enum EquipmentMeshRegion { Legs, Arms, Torso } // corresponds to body blendshapes
