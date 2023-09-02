using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image itemPreview;

    private void Start()
    {
        itemPreview = GetComponentInChildren<Image>();
    }

    public void SetItem(Item item)
    {
        itemPreview.sprite = item.sprite;
        itemPreview.color = new Color(1, 1, 1, 1);
    }

    public void ClearSlot()
    {
        itemPreview.sprite = null;
        itemPreview.color = new Color(1, 1, 1, 0);
    }
}
