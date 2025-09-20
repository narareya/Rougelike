using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public InputAction inventoryAction;
    public GameObject InventoryMenu;
    private bool menuActivated;

    private static InventoryManager instance;
    [SerializeField] private ItemSlot[] itemSlots;

    void Awake()
    {
        inventoryAction = new InputAction("Inventory", binding: "<Keyboard>/i");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        inventoryAction.Enable();
        inventoryAction.performed += ToggleInventory;
    }

    void OnDisable()
    {
        inventoryAction.performed -= ToggleInventory;
        inventoryAction.Disable();
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        menuActivated = !menuActivated;
        InventoryMenu.SetActive(menuActivated);
        Time.timeScale = menuActivated ? 0f : 1f;
    }

    public static void AddItem(string itemName, Sprite itemSprite, int quantity)
    {
        if (instance == null)
        {
            Debug.LogWarning("InventoryManager instance is not set.");
            return;
        }

        foreach (ItemSlot slot in instance.itemSlots)
        {
            if (slot != null && !slot.isFull)
            {
                slot.AddItem(itemName, quantity, itemSprite);
                Debug.Log($"Added {itemName} to inventory.");
                return;
            }

        }
        Debug.Log("No empty slot available in the inventory.");

        // for (int i = 0; i < instance.itemSlots.Length; i++)
        // {
        //     if (!instance.itemSlots[i].isFull)
        //     {
        //         instance.itemSlots[i].AddItem(itemName, quantity, itemSprite);
        //         Debug.Log($"Added {itemName} to slot {i}.");
        //         return;
        //     }
        // }

    }
}
