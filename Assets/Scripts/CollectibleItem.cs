using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private int quantity = 1;

    void Start()
    {
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Add the item to the inventory
            InventoryManager.AddItem(itemName, itemIcon, quantity);
            
            // Destroy the collectible item from the scene
            Destroy(gameObject);
        }
    }
}
