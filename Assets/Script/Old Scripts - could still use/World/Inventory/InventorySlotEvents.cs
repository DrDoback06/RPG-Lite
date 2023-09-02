using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotEvents : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private EquipmentSlot equipmentSlot;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (equipmentSlot != null)
        {
            inventoryUI.BeginDragItem(equipmentSlot.itemPreview.sprite);
        }
        else
        {
            inventoryUI.BeginDragItem(GetComponentInParent<ItemSlot>().itemPreview.sprite);
        }
    }


    private void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        inventoryUI.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventoryUI.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inventoryUI.OnEndDrag(eventData);
    }

    public void OnPointerDownHandler(PointerEventData eventData)
    {
        inventoryUI.OnPointerDown(eventData, equipmentSlot);
    }

    public void OnPointerUpHandler(PointerEventData eventData)
    {
        inventoryUI.OnPointerUp(eventData, equipmentSlot);
    }

}
