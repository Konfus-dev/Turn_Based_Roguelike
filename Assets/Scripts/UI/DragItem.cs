using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] 
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Item item;
    public Inventory inventory;
    public Transform parent;

    void Awake()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = rectTransform.transform.parent;
        rectTransform.transform.parent = GameObject.FindGameObjectWithTag("DragItem").transform;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.transform.parent = parent;
        if (eventData.pointerDrag.transform.parent.tag == "EquipSlot") eventData.pointerDrag.transform.parent.GetComponent<EquipSlot>().background.enabled = false;
        rectTransform.anchoredPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<RectTransform>().parent = eventData.pointerDrag.GetComponent<DragItem>().parent;
    }
   
}

