using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    public Item.EquipSlot acceptedEquipSlot; // The accepted equip slot for this equipment slot
    public Item currentItem;

    public Image itemPreview;
    private Image backgroundImage;
    public InventoryUI inventoryUI;
    public Item equippedItem;

    private void Start()
    {
        backgroundImage = GetComponent<Image>();
        itemPreview = transform.Find("ItemPreview").GetComponent<Image>();
        inventoryUI = FindObjectOfType<InventoryUI>();

        if (inventoryUI == null)
        {
            Debug.LogError("EquipmentSlot: Unable to find InventoryUI component in parent.");
        }

        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((eventData) => inventoryUI.OnPointerDown((PointerEventData)eventData, this));
        eventTrigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((eventData) => inventoryUI.OnPointerUp((PointerEventData)eventData, this));
        eventTrigger.triggers.Add(pointerUpEntry);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (inventoryUI.draggedItem != null && acceptedEquipSlot == inventoryUI.draggedItem.equipSlot)
        {
            Item previousItem = equippedItem;
            equippedItem = inventoryUI.draggedItem;

            itemPreview.sprite = equippedItem.sprite;
            itemPreview.color = new Color(1, 1, 1, 1);

            backgroundImage.enabled = false;

            // Update character model
            CharacterModel characterModel = FindObjectOfType<CharacterModel>();
            characterModel.EquipItem(acceptedEquipSlot, equippedItem);

            // Remove the equipped item from the inventory
            inventoryUI.inventory.RemoveItem(equippedItem);
            inventoryUI.UpdateUI();

            // If there was an item already equipped in the slot, swap it with the dragged item
            if (previousItem != null)
            {
                inventoryUI.inventory.AddItem(previousItem);
                inventoryUI.UpdateUI();
            }
        }
    }
}
