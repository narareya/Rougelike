using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public string description;
    public Sprite icon;

    public InventoryItem(string name, string desc, Sprite sprite)
    {
        itemName = name;
        description = desc;
        icon = sprite;
    }
}