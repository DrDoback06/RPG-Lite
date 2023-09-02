using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Inventory inventory;
    public GameObject inventoryPanel;
    public GameObject itemSlotPrefab;
    public GameObject[] itemSlots;
    public Transform groundItemParent;

    private Dictionary<Item, GameObject> itemSlotLookup;
    public Item draggedItem;
    public bool IsInventoryActive;

    [SerializeField] private CharacterModel characterModel;
    [SerializeField] private Image draggedItemPreview;
    [SerializeField] private List<EquipmentSlot> equipmentSlots;


    private void Start()
    {
        itemSlotLookup = new Dictionary<Item, GameObject>();

        itemSlots = new GameObject[inventory.gridWidth * inventory.gridHeight];

        for (int i = 0; i < inventory.gridWidth * inventory.gridHeight; i++)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, inventoryPanel.transform);
            itemSlot.name = "ItemSlot_" + i;
            itemSlot.SetActive(true);
            itemSlots[i] = itemSlot;
        }

        if (characterModel == null)
        {
            characterModel = GetComponent<CharacterModel>();
        }
        
        UpdateUI();
    }


    private void Update()
    {
        if (draggedItem != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            draggedItemPreview.rectTransform.position = mousePosition;
        }
    }

    public void UpdateUI()
    {
        // Clear previous item slots
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].GetComponent<Image>().sprite = null;
        }

        // Populate item slots with items in inventory
        int itemIndex = 0;
        foreach (var item in inventory.items)
        {
            itemSlots[itemIndex].GetComponent<Image>().sprite = item.sprite;
            itemSlotLookup[item] = itemSlots[itemIndex];
            itemIndex++;
        }
    }

    public void OnPointerDown(PointerEventData eventData, EquipmentSlot equipmentSlot = null)
    {
        if (draggedItem == null)
        {
            foreach (var itemSlot in itemSlotLookup)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(itemSlot.Value.GetComponent<RectTransform>(), eventData.position))
                {
                    draggedItem = itemSlot.Key;
                    break;
                }
            }
        }

        if (draggedItem != null)
        {
            draggedItemPreview.sprite = draggedItem.sprite;
            draggedItemPreview.enabled = true;

            // Remove the item from the original slot
            itemSlotLookup[draggedItem].GetComponent<Image>().enabled = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData, EquipmentSlot equipmentSlot = null)
    {
        if (draggedItem != null)
        {
            EquipmentSlot targetEquipmentSlot = null;
            GameObject targetInventorySlot = null;
            Item targetItem = null;

            foreach (var eslot in equipmentSlots)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(eslot.GetComponent<RectTransform>(), eventData.position))
                {
                    targetEquipmentSlot = eslot;
                    break;
                }
            }

            foreach (var itemSlot in itemSlots)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(itemSlot.GetComponent<RectTransform>(), eventData.position))
                {
                    targetInventorySlot = itemSlot;
                    break;
                }
            }

            if (targetEquipmentSlot != null)
            {
                if (targetEquipmentSlot.acceptedEquipSlot == draggedItem.equipSlot)
                {
                    Item currentItem = characterModel.UnequipItem(draggedItem.equipSlot);
                    characterModel.EquipItem(draggedItem.equipSlot, draggedItem);

                    if (currentItem != null)
                    {
                        inventory.AddItem(currentItem);
                    }

                    inventory.RemoveItem(draggedItem);
                    UpdateUI();
                }
                else
                {
                    // If the equipment slot is not compatible, place the item back in the inventory
                    inventory.AddItem(draggedItem);
                }
            }
            else if (targetInventorySlot != null)
            {
                // Remove the item from the original slot
                itemSlotLookup.Remove(draggedItem);

                // If there is a target item, swap the items
                foreach (var itemSlot in itemSlotLookup)
                {
                    if (itemSlot.Value == targetInventorySlot)
                    {
                        targetItem = itemSlot.Key;
                        break;
                    }
                }

                if (targetItem != null)
                {
                    inventory.SwapItems(draggedItem, targetItem);
                    itemSlotLookup[targetItem] = itemSlotLookup[draggedItem];
                }

                // Add the item to the target slot
                itemSlotLookup.Add(draggedItem, targetInventorySlot);
                UpdateUI();
            }
            else if (equipmentSlot != null && equipmentSlot.acceptedEquipSlot == draggedItem.equipSlot)
            {
                Item unequippedItem = characterModel.UnequipItem(draggedItem.equipSlot);
                inventory.AddItem(unequippedItem);
                UpdateUI();
            }
            else
            {
                // Throw item on the ground
                ThrowItemOnGround(draggedItem);
                inventory.RemoveItem(draggedItem);
                UpdateUI();
            }

            draggedItemPreview.enabled = false;
            draggedItem = null;

            // Re-enable the item image for all item slots
            foreach (var itemSlot in itemSlotLookup.Values)
            {
                itemSlot.GetComponent<Image>().enabled = true;
            }
        }
    }

    public void BeginDragItem(Sprite sprite)
    {
        draggedItemPreview.sprite = sprite;
        draggedItemPreview.enabled = true;
    }

    public void DragItem(Vector2 position)
    {
        draggedItemPreview.rectTransform.position = position;
    }

    public void EndDragItem()
    {
        draggedItemPreview.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnPointerDown(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            draggedItemPreview.rectTransform.position = mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnPointerUp(eventData);
    }

    public void BeginDragEquipmentItem(EquipmentSlot equipmentSlot)
    {
        if (equipmentSlot.currentItem != null)
        {
            draggedItem = equipmentSlot.currentItem;
            draggedItemPreview.sprite = draggedItem.sprite;
            draggedItemPreview.enabled = true;
        }
    }


    private void ThrowItemOnGround(Item item)
        {
            // Instantiate a game object representing the item on the ground
            GameObject itemOnGround = new GameObject(item.name);
            itemOnGround.transform.position = new Vector3(/* Specify the position where you want to throw the item */);
            itemOnGround.transform.SetParent(groundItemParent);

            // Add any necessary components (e.g., sprite renderer, collider) to the itemOnGround
            // based on the specific requirements of your game
        }
    }