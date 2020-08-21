using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_n_Drop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] 
    private Canvas Canvas;
    private RectTransform RectTransform;
    private CanvasGroup CanvasGroup;
    private Item Item;
    //private ItemSlot Slot;
    private Inventory Inventory;
    public Vector2 OriginalPos;

    void Awake()
    {
        this.RectTransform = this.GetComponent<RectTransform>();
        this.CanvasGroup = this.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Clicked");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        this.OriginalPos = RectTransform.anchoredPosition;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.alpha = .5f;
        /*if(Slot.BackgroundIcon != null)
            Slot.BackgroundIcon.color = new Color(Slot.BackgroundIcon.color.r, Slot.BackgroundIcon.color.b, Slot.BackgroundIcon.color.g, 72/255);*/
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        this.RectTransform.anchoredPosition += eventData.delta / this.Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        /*if (Slot.BackgroundIcon != null)
            Slot.BackgroundIcon.color = new Color(Slot.BackgroundIcon.color.r, Slot.BackgroundIcon.color.b, Slot.BackgroundIcon.color.g, 0);*/

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

    /*public void SetSlot(ItemSlot slot)
    {
        this.Slot = slot;
    }

    public ItemSlot GetSlot()
    {
        return this.Slot;
    }*/

    public Item GetItem()
    {
        return this.Item;
    }

    public Inventory GetInventory()
    {
        return this.Inventory;
    }
}

