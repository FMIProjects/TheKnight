using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragDropScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image img;
    private Text text;
    private int counter = 1;
    public Transform originalPos;
    [SerializeField] private int maxCount = 8;

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        img.raycastTarget = false;
        originalPos = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        img.raycastTarget = true;
        transform.SetParent(originalPos);
    }

    public void RefreshCount(int byHowMuch)
    {
        counter += byHowMuch;
        text.text = counter.ToString();
    }

    public int GetCount()
    {
        return counter;
    }

    public bool IsFull(int howMuchToAdd)
    {
        return counter + howMuchToAdd > maxCount;
    }
}
