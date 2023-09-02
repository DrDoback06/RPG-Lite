using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    public Dictionary<Item.EquipSlot, SpriteRenderer> equipmentRenderers;
    private Dictionary<Item.EquipSlot, Item> equippedItems; // Dictionary to store the currently equipped items

    private void Awake()
    {
        equipmentRenderers = new Dictionary<Item.EquipSlot, SpriteRenderer>();
        equippedItems = new Dictionary<Item.EquipSlot, Item>(); // Initialize the equippedItems dictionary

        // Populate the dictionary with all the equipment slots and their corresponding SpriteRenderers
        equipmentRenderers[Item.EquipSlot.Helm] = transform.Find("HelmSpriteRenderer").GetComponent<SpriteRenderer>();
        equipmentRenderers[Item.EquipSlot.Amulet] = transform.Find("AmuletSpriteRenderer").GetComponent<SpriteRenderer>();
        equipmentRenderers[Item.EquipSlot.Armor] = transform.Find("ArmorSpriteRenderer").GetComponent<SpriteRenderer>();
        equipmentRenderers[Item.EquipSlot.WeaponLeft] = transform.Find("WeaponLeftSpriteRenderer").GetComponent<SpriteRenderer>();
        equipmentRenderers[Item.EquipSlot.WeaponRight] = transform.Find("WeaponRightSpriteRenderer").GetComponent<SpriteRenderer>();
        equipmentRenderers[Item.EquipSlot.Gloves] = transform.Find("GlovesSpriteRenderer").GetComponent<SpriteRenderer>();
        equipmentRenderers[Item.EquipSlot.Boots] = transform.Find("BootsSpriteRenderer").GetComponent<SpriteRenderer>();
        equipmentRenderers[Item.EquipSlot.Belt] = transform.Find("BeltSpriteRenderer").GetComponent<SpriteRenderer>();
        equipmentRenderers[Item.EquipSlot.RingLeft] = transform.Find("RingLeftSpriteRenderer").GetComponent<SpriteRenderer>();
        equipmentRenderers[Item.EquipSlot.RingRight] = transform.Find("RingRightSpriteRenderer").GetComponent<SpriteRenderer>();
        equipmentRenderers[Item.EquipSlot.Charm] = transform.Find("CharmSpriteRenderer").GetComponent<SpriteRenderer>();
        // Add more equipment slots as needed
    }

    public Item EquipItem(Item.EquipSlot slotType, Item item)
    {
        Item previousItem = null;

        if (equipmentRenderers.ContainsKey(slotType))
        {
            // Check if there's an item already equipped in this slot
            if (equippedItems.ContainsKey(slotType))
            {
                previousItem = equippedItems[slotType]; // Store the currently equipped item to return it later
                UnequipItem(slotType); // Unequip the currently equipped item
            }

            equipmentRenderers[slotType].sprite = item.sprite;
            equipmentRenderers[slotType].enabled = true;
            equippedItems[slotType] = item; // Add the new item to the equippedItems dictionary
        }

        return previousItem; // Return the previously equipped item (if any)
    }

    public Item UnequipItem(Item.EquipSlot slotType)
    {
        Item unequippedItem = null;

        if (equipmentRenderers.ContainsKey(slotType) && equippedItems.ContainsKey(slotType))
        {
            unequippedItem = equippedItems[slotType]; // Store the item to return it later
            equipmentRenderers[slotType].sprite = null;
            equipmentRenderers[slotType].enabled = false;
            equippedItems.Remove(slotType); // Remove the item from the equippedItems dictionary
        }

        return unequippedItem; // Return the unequipped item
    }
}
