using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject signCanvas;  // Assign the Canvas GameObject in the Inspector
    public House house;  // Reference to the House script attached to the same GameObject
    private bool isPlayerNear = false;

    void Start()
    {
        if (signCanvas != null)
        {
            signCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.B))
        {
            if (house != null)
            {
                house.BuildHouse();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (signCanvas != null)
            {
                signCanvas.SetActive(true);  // Show the canvas when player is near
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (signCanvas != null)
            {
                signCanvas.SetActive(false);  // Hide the canvas when player moves away
            }
        }
    }
}
