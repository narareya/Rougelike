using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    // Data yang disimpandi setiap slot inventory
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;

    // Referensi ke komponen UI
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // public void AddItem(string itemName, int quantity, Sprite itemSprite)
    // {
    //     this.itemName = itemName;
    //     this.quantity = quantity;
    //     this.itemSprite = itemSprite;
    //     isFull = false;

    //     quantityText.text = quantity.ToString();
    //     quantityText.enabled = true;
    //     itemImage.sprite = itemSprite;

    // }

    public void AddItem(string newItemName, int newQuantity, Sprite newItemSprite)
    {
        if (!isFull)
        {
            itemName = newItemName;
            quantity += newQuantity;
            itemSprite = newItemSprite;
            isFull = true;

            Debug.LogWarning("Slot is full with a different item.");
            return;
        }
        else if (itemName == newItemName)
        {
            quantity += newQuantity;
        }

        // Update UI
        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = newItemSprite;
        itemImage.enabled = true;
    }
}