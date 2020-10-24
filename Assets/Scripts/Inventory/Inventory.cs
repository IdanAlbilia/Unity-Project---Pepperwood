using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; 
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singelton

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than once instances of inventory were found!");
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public static Inventory instance;

    public int space = 20; 

    public List<Item> items = new List<Item>();

    

    public bool Add(Item item)
    {
        if (!item.isDefaultItem){
            if(items.Count >= space)
            {
                Debug.Log("not enough inventory room");
                return false;
            }
            items.Add(item);

            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }


}
