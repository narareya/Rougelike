using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private int quantity = 1;

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Add the item to the inventory
            InventoryManager.AddItem(itemName, itemSprite, quantity);

            // Destroy the collectible item from the scene
            Destroy(gameObject);
        }
    }
}
