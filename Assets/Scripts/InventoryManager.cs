using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public InputAction inventoryAction;
    public GameObject InventoryMenu;
    private bool menuActivated;

    public List<InventoryItem> items = new List<InventoryItem>();

    void Awake()
    {
        inventoryAction = new InputAction("Inventory", binding: "<Keyboard>/i");
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
}
