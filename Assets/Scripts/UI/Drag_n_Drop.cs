using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_n_Drop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] 
    private Canvas Canvas;
    private RectTransform RectTransform;
    private CanvasGroup CanvasGroup;
    private Item Item;
    private Inventory Inventory;
    private Inventory EquipedItems;
    private Vector2 OriginalPos;

    void Awake()
    {
        this.RectTransform = this.GetComponent<RectTransform>();
        this.CanvasGroup = this.GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.OriginalPos = RectTransform.anchoredPosition;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.alpha = .5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.RectTransform.anchoredPosition += eventData.delta / this.Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.RectTransform.anchoredPosition = this.OriginalPos;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1;
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<Drag_n_Drop>().OriginalPos;
    }

    public void SetItem(Item item)
    {
        this.Item = item;
    }

    public void SetInventory(Inventory inv)
    {
        this.Inventory = inv;
    }

    public void SetEquipedItems(Inventory equiped)
    {
        this.EquipedItems = equiped;
    }

    public Inventory GetEquipedItems()
    {
        return this.EquipedItems;
    }

    public Vector2 GetOrigPos()
    {
        return this.OriginalPos;
    }

    public void SetOrigPos(Vector2 pos)
    {
        this.OriginalPos = pos;
    }

    public Item GetItem()
    {
        return this.Item;
    }

    public Inventory GetInventory()
    {
        return this.Inventory;
    }
}

