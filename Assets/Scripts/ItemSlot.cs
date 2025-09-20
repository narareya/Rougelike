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

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = false;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;

    }
}