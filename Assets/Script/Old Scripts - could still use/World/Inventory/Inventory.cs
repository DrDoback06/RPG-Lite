using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int gridWidth = 8;
    public int gridHeight = 4;

    [SerializeField]
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }

    public void SwapItems(Item firstItem, Item secondItem)
    {
        int firstItemIndex = items.IndexOf(firstItem);
        int secondItemIndex = items.IndexOf(secondItem);

        if (firstItemIndex != -1 && secondItemIndex != -1)
        {
            items[firstItemIndex] = secondItem;
            items[secondItemIndex] = firstItem;
        }
        else if (firstItemIndex != -1)
        {
            items.RemoveAt(firstItemIndex);
            items.Add(secondItem);
        }
        else if (secondItemIndex != -1)
        {
            items.RemoveAt(secondItemIndex);
            items.Add(firstItem);
        }
    }
}
