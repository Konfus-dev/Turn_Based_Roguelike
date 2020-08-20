using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_n_Drop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] 
    private Canvas Canvas;
    private RectTransform RectTransform;
    private CanvasGroup CanvasGroup;
    private Item Item;
    private Inventory Inventory;

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
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.alpha = .5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        this.RectTransform.anchoredPosition += eventData.delta / this.Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Dropped");
    }

    public void SetItem(Item item)
    {
        this.Item = item;
    }

    public void SetInventory(Inventory inv)
    {
        this.Inventory = inv;
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

