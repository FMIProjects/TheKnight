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
        Debug.Log("The inventory has been opened and text has been initialized");
        // Get the text components of the item child ( counter )
        text = transform.GetChild(0).GetComponent<Text>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Disable the counter of the item
        transform.GetChild(0).gameObject.SetActive(false);
        img.raycastTarget = false;
        // Set the parent of the item to the root of the canvas
        originalPos = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Set the position of the item to the mouse position
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Enable the counter of the item
        transform.GetChild(0).gameObject.SetActive(true);
        img.raycastTarget = true;
        transform.SetParent(originalPos);
    }

    public void RefreshCount(int byHowMuch)
    {
        // Get the text components of the item child ( counter )
        // start will not always be called before this function
        text = transform.GetChild(0).GetComponent<Text>();

        counter += byHowMuch;
        //if(text != null)
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
