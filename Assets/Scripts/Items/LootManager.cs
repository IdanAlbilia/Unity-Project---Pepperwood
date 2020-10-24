using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    #region Singleton
    public static LootManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public ItemPickup[] lootItems;

    public void DropItem(Vector3 position)
    {
        int randomItem = Random.Range(0, lootItems.Length);
        Debug.Log("creating an item");
        Instantiate<ItemPickup>(lootItems[randomItem], new Vector3(position.x, position.y + 0.5f, position.z), Quaternion.Euler(-45f, 0f, 0f));
    }
}
