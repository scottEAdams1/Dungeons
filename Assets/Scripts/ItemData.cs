using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    /// <summary>
    /// Name of the item
    /// </summary>
    public string itemName;

    /// <summary>
    /// Image of the item
    /// </summary>
    public Sprite icon;

    /// <summary>
    /// Whether the item is stackable
    /// </summary>
    public bool isStackable;

    /// <summary>
    /// The max amount the item can be stacked to in a single slot, default 512
    /// </summary>
    public int maxStack = 512;

    /// <summary>
    /// The prefab of the object to interact with the scene
    /// </summary>
    public GameObject prefab;
}
