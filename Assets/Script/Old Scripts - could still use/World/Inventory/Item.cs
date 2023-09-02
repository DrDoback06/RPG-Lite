using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Helm, Amulet, Armor, WeaponLeft, WeaponRight, Gloves, Boots, Belt, RingLeft, RingRight, Charm, Potion, Ranged, Gem, Rune
    }

    public enum ItemRarity
    {
        Normal, Exceptional, Superior, Rare, Unique
    }

    public string itemName;
    public ItemType itemType;
    public ItemRarity itemRarity;
    public Sprite sprite;
    public bool isEquippable;
    public bool isStackable;
    public int maxStackSize = 1; // For stackable items
    public Vector2Int gridSize; // Width and height in inventory grid

    // Basic properties
    public int armorValue;
    public int attackPower;
    public int durability;
    public int maxDurability;

    // Potion properties
    public enum PotionEffect { Health, Mana, Stamina }
    public PotionEffect effect;
    public int potency;
    public float duration;

    // Equipment properties
    public enum EquipSlot { None, Helm, Amulet, Armor, WeaponLeft, WeaponRight, Gloves, Boots, Belt, RingLeft, RingRight, Charm }
    public EquipSlot equipSlot;

    // Bonus properties
    public int bonusStrength;
    public int bonusDexterity;
    public int bonusVitality;
    public int bonusEnergy;

    // Elemental damage/resistance properties
    public int fireDamage;
    public int coldDamage;
    public int lightningDamage;
    public int poisonDamage;

    public int fireResistance;
    public int coldResistance;
    public int lightningResistance;
    public int poisonResistance;

    // Other properties
    public float fasterHitRecovery;
    public float increasedAttackSpeed;
    public float fasterCastRate;
    public float lifeSteal;
    public float manaSteal;

    // You can add even more properties as needed
}
