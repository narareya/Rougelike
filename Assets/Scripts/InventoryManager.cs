using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public InputAction OpenInventory;
    public GameObject InventoryMenu;
    private bool menuActivated;

    public GameObject itemSlotPrefab;
    public Transform inventorySlotsParent;

    private List<ItemSlot> itemSlots = new List<ItemSlot>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        OpenInventory = new InputAction("OpenInventory", binding: "<Keyboard>/I");
    }

    void OnEnable()
    {
        OpenInventory.Enable();
        OpenInventory.performed += ToggleInventory;
    }

    void OnDisable()
    {
        OpenInventory.performed -= ToggleInventory;
        OpenInventory.Disable();
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        menuActivated = !menuActivated;
        InventoryMenu.SetActive(menuActivated);
        Time.timeScale = menuActivated ? 0f : 1f;
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.itemName == itemName)
            {
                slot.AddItem(itemName, quantity, itemSprite);
                Debug.Log($"Added {quantity} of {itemName} to inventory.");
                return;
            }
        }

        GameObject newSlotObj = Instantiate(itemSlotPrefab, inventorySlotsParent);
        ItemSlot newSlot = newSlotObj.GetComponent<ItemSlot>();
        newSlot.AddItem(itemName, quantity, itemSprite);
        itemSlots.Add(newSlot);
        Debug.Log($"Created new slot and added {quantity} of {itemName} to inventory.");
    }
}
