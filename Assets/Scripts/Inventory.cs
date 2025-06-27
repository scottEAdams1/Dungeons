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

    private void Awake()
    {
        // If inventory is uninitialised, create inventory slots
        if (inventory.Count == 0)
        {
            for (int i = 0; i < size; i++)
            {
                inventory.Add(new InventorySlot());
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem(ItemData item, int amount = 1)
    {
        for (int i = 0; i < size; i++)
        {
            int quantity = inventory[i].quantity;
            bool successful = inventory[i].AddItem(item, amount);
            if (successful)
            {
                return true;
            }
            int added = inventory[i].quantity - quantity;
            amount -= added;
            if (amount <= 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(ItemData item, int amount = 1)
    {
        for (int i = 0; i < size; i++)
        {
            int quantity = inventory[i].quantity;
            bool successful = inventory[i].RemoveItem(item, amount);
            if (successful)
            {
                return true;
            }
            int added = inventory[i].quantity - quantity;
            amount -= added;
            if (amount <= 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasItem(ItemData item, int amount = 1)
    {
        int total = 0;
        for (int i = 0; i < size; i++)
        {
            if (inventory[i].DataItem == item)
            {
                total += inventory[i].quantity;
            }
            if (total >= amount)
            {
                return true;
            }
        }
        return false;
    }

    public void SwapSlots(int index1, int index2)
    {
        InventorySlot temp = inventory[index1];
        inventory[index1] = inventory[index2];
        inventory[index2] = temp;
    }

    public InventorySlot GetItemAt(int index)
    {
        return inventory[index];
    }

    public void ClearSlot(int index)
    {
        inventory[index].Clear();
    }
}
