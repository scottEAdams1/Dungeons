using NUnit.Framework;
using UnityEditor.Rendering;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Inventory
    /// </summary>
    public List<InventorySlot> inventory = new List<InventorySlot>();

    /// <summary>
    /// Size of inventory, default 36
    /// </summary>
    public int size = 36;

    public ItemData swordData;

    private void Awake()
    {
        // If inventory is uninitialised, create inventory slots
        if (inventory.Count == 0)
        {
            for (int i = 0; i < size; i++)
            {
                inventory.Add(new GameObject("InventorySlot").AddComponent<InventorySlot>());
            }
        }
        inventory[0].AddItem(swordData, 1);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Add an item to the inventory
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <param name="amount">The amount of the item to add</param>
    /// <returns>True if it succeeeds, false otherwise</returns>
    public bool AddItem(ItemData item, int amount = 1)
    {
        // Loop through the inventory slots
        for (int i = 0; i < size; i++)
        {
            // Try to add the item to the slot
            if (inventory[i].ItemData == item)
            {
                int quantity = inventory[i].quantity;
                bool successful = inventory[i].AddItem(item, amount);
                if (successful)
                {
                    return true;
                }

                // If not all of the item was added (max stack reached), remove the amount successfully added from the amount to add
                int added = inventory[i].quantity - quantity;
                amount -= added;

                // If no more to add, return true
                if (amount <= 0)
                {
                    return true;
                }
            }
        }

        // If not all the amount of item was fully added, return false
        return false;
    }

    /// <summary>
    /// Remove an item from inventory
    /// </summary>
    /// <param name="item">The item to remove</param>
    /// <param name="amount">The amount to remove</param>
    /// <returns>True if successfully removed, false otherwise</returns>
    public bool RemoveItem(ItemData item, int amount = 1)
    {
        // Loop through the inventory
        for (int i = 0; i < size; i++)
        {
            // Try to remove the item from the slot
            if (inventory[i].ItemData == item)
            {
                int quantity = inventory[i].quantity;
                bool successful = inventory[i].RemoveItem(amount);
                if (successful)
                {
                    return true;
                }

                // If not all of the item was removed (not enough in a single slot), remove the amount successfully removed from the amount to remove
                int added = inventory[i].quantity - quantity;
                amount -= added;

                // If no more to remove, return true
                if (amount <= 0)
                {
                    return true;
                }
            }
        }

        // If not all the amount was fully removed, return false
        return false;
    }

    /// <summary>
    /// Check if inventory contains item
    /// </summary>
    /// <param name="item">Item to look for</param>
    /// <param name="amount">Amount of item to look for</param>
    /// <returns>True if amount lot of item was found, false otherwise</returns>
    public bool HasItem(ItemData item, int amount = 1)
    {
        // Loop through the inventory
        int total = 0;
        for (int i = 0; i < size; i++)
        {
            // If inventory slot contains item, add to toal
            if (inventory[i].ItemData == item)
            {
                total += inventory[i].quantity;
            }

            // If at least amount is found in inventory, return true
            if (total >= amount)
            {
                return true;
            }
        }

        // If total is less than amount by time all of inventory is searched, return false
        return false;
    }

    /// <summary>
    /// Swap items between two slots
    /// </summary>
    /// <param name="index1">Index of slot 1 to swap with</param>
    /// <param name="index2">Index of slot 2 to swap with</param>
    public void SwapSlots(int index1, int index2)
    {
        InventorySlot temp = inventory[index1];
        inventory[index1] = inventory[index2];
        inventory[index2] = temp;
    }

    /// <summary>
    /// Get the item held in an inventory slot
    /// </summary>
    /// <param name="index">Index of inventory slot to search</param>
    /// <returns>The inventory slot at index</returns>
    public InventorySlot GetItemAt(int index)
    {
        return inventory[index];
    }

    /// <summary>
    /// Clear an inventory slot
    /// </summary>
    /// <param name="index">Index of inventory slot to clear</param>
    public void ClearSlot(int index)
    {
        inventory[index].Clear();
    }
}
