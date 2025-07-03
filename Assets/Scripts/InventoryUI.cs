using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// Player's inventory
    /// </summary>
    public Inventory inventory;

    /// <summary>
    /// Template of slot UI for inventory
    /// </summary>
    public VisualTreeAsset slotTemplate;

    /// <summary>
    /// Parent to place inventory slots into
    /// </summary>
    private VisualElement gridRoot;

    private void OnEnable()
    {
        // Get parent to place inventory slots into
        gridRoot = GetComponent<UIDocument>().rootVisualElement.Q("inventory-grid");

        // Loop through the inventory
        for (int i = 0; i < inventory.size; i++)
        {
            // Clone a slot for the inventory
            VisualElement slot = slotTemplate.CloneTree();
            slot.name = "inventory-slot";
            // Fill slot with item
            UpdateSlot(slot, inventory.GetItemAt(i));
            // Add slot to parent
            gridRoot.Add(slot);
        }
    }

    /// <summary>
    /// Update slot with item data
    /// </summary>
    /// <param name="slot">Inventory slot to fill</param>
    /// <param name="data">Data to fill slot with</param>
    void UpdateSlot(VisualElement slot, InventorySlot data)
    {
        // Get icon and quantity elements from UI slot
        var icon = slot.Q<VisualElement>("icon");
        var quantity = slot.Q<Label>("quantity");

        // If data exists for the slot, fill in correct data
        if (data.ItemData != null)
        {
            icon.style.backgroundImage = new StyleBackground(data.ItemData.icon.texture);
            icon.style.opacity = 1f;
            quantity.text = data.ItemData.isStackable ? data.quantity.ToString() : "";
        }
        else
        {
            icon.style.opacity = 0f;
            quantity.text = "";
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
}
