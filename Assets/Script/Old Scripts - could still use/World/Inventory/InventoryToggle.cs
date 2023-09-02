using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryUIObject;
    public KeyCode toggleKey = KeyCode.I;

    private bool inventoryVisible;

    private void Start()
    {
        // Set inventory UI to not visible by default
        inventoryVisible = false;
        inventoryUIObject.SetActive(false);

        InventoryUI inventoryUI = inventoryUIObject.GetComponent<InventoryUI>();
        if (inventoryUI != null)
        {
            inventoryUI.IsInventoryActive = true;
            inventoryUI.IsInventoryActive = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        inventoryVisible = !inventoryVisible;
        inventoryUIObject.SetActive(inventoryVisible);
    }
}
