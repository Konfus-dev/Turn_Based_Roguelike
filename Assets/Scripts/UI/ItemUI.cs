using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    private Canvas canvas;
    [SerializeField]
    private GameObject InfoPopup;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public ItemData item;
    public Inventory itemInventory;
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
        if (eventData.pointerDrag.transform.parent.CompareTag("EquipSlot")) eventData.pointerDrag.transform.parent.GetComponent<EquipSlot>().background.enabled = false;
        rectTransform.anchoredPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemUI itemDropped = eventData.pointerDrag.GetComponent<ItemUI>();

        eventData.pointerDrag.GetComponent<RectTransform>().parent = itemDropped.parent;
        
        if (item.itemName == itemDropped.item.itemName && item.amount != item.maxStackAmount)
        {
            item.amount += itemDropped.item.amount;
            Debug.Log("HIYA");
            if(item.amount > item.maxStackAmount)
            {
                itemDropped.item.amount = (item.amount - item.maxStackAmount); 
                item.amount = item.maxStackAmount;

                TMP_Text text = this.GetComponent<RectTransform>().Find("Number").GetComponent<TMP_Text>();

                text.text = item.amount.ToString();
            }

            if (itemDropped.item.amount <= 0)
            {
                itemDropped.itemInventory.RemoveItem(itemDropped.item, itemDropped.gameObject, false);
                Destroy(itemDropped.gameObject);
            }
            else
            {
                TMP_Text droppedText = eventData.pointerDrag.transform.Find("Number").GetComponent<TMP_Text>();

                if (itemDropped.item.amount > 1)
                {
                    droppedText.text = item.amount.ToString();
                }
                else
                {
                    droppedText.text = "";
                }
            }
        }
    }

    //show item description popup here
    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    //hide item description popup here
    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}

