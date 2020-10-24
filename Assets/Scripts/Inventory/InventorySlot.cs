using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public Button removeButton;
    public Image icon;
    Item item;

    public Text hoverText;
    public Image HoverBackground;
    public Image HoverBorder;

    // Start is called before the first frame update
    void Start()
    {
        hoverText.enabled = false;
        HoverBorder.enabled = false;
        HoverBackground.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void onRemoveButton()
    {
        Inventory.instance.Remove(item);

    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void OnMouseEnter()
    {
        if (item)
        {
            hoverText.enabled = true;
            HoverBackground.enabled = true;
            HoverBorder.enabled = true;
            hoverText.text = item.name;
        }
    }

    public void OnMouseExit()
    {
        hoverText.enabled = false;
        HoverBackground.enabled = false;
        HoverBorder.enabled = false;
    }
}
