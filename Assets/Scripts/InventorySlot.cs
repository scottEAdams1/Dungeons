using JetBrains.Annotations;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    /// <summary>
    /// The item data for the slot
    /// </summary>
    public ItemData ItemData;

    /// <summary>
    /// The quantity in the slot
    /// </summary>
    public int quantity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Whether any more can be stacked in the slot
    /// </summary>
    /// <param name="item">The item to stack</param>
    /// <returns>True if the item can be added to the slot, false otherwise</returns>
    public bool CanStack(ItemData item)
    {
        return item.isStackable // If the item is stackable
            && ItemData != null // If the inventory slot has anything in it
            && item == ItemData // If the item trying to be stacked is the same as the one in the slot
            && quantity < ItemData.maxStack; // If there is room to stack any more
    }

    /// <summary>
    /// Adding the item to the slot
    /// </summary>
    /// <param name="item">The item to add to the slot</param>
    /// <param name="amount">The amount to add to the slot</param>
    /// <returns>True if all of the item can be add, false otherwise</returns>
    public bool AddItem(ItemData item, int amount)
    {
        // If the slot is empty, set it to the item and amount, return true
        if (ItemData == null)
        {
            ItemData = item;
            quantity = amount;
            return true;
        }

        // If the item to be added can be stacked, add as much as you can, if it all fits, return true, else return false
        if (CanStack(item))
        {
            int availableSpace = ItemData.maxStack - quantity;
            int toAdd = Mathf.Min(availableSpace, amount);
            quantity += toAdd;
            return toAdd == amount;
        }

        // Item can't be added, return false
        return false;
    }

    /// <summary>
    /// Removing some of the item from the slot
    /// </summary>
    /// <param name="amount">The amount to remove</param>
    /// <returns>True if successfully removed amount, false otherwise</returns>
    public bool RemoveItem(int amount)
    {
        // If there is at least the amount wanting to be removed in the slot, remove that amount
        if (quantity >= amount)
        {
            quantity -= amount;
            // If all is removed, clear the slot
            if (quantity == 0)
            {
                Clear();
            }
            return true;
        }
        // Trying to remove more than exists, return false
        return false;
    }

    /// <summary>
    /// Clear an inventory slot, set variables to null
    /// </summary>
    public void Clear()
    {
        ItemData = null;
        quantity = 0;
    }
}
