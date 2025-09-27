using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] public Image itemImage;
    [SerializeField] public TMP_Text itemQuantityText;

    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void AddItem(string newItemName, int newQuantity, Sprite newItemSprite)
    {
        if (itemName == newItemName)
        {
            quantity += newQuantity;

        }
        else
        {
            itemName = newItemName;
            quantity = newQuantity;
            itemSprite = newItemSprite;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        itemQuantityText.text = quantity.ToString();
        itemImage.enabled = true;
        itemImage.sprite = itemSprite;

    }
}
