using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public House house;  // Reference to the House script attached to the same GameObject

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        Debug.Log(button);

        button?.onClick.AddListener(OnBuildButtonClicked);
    }

    public void OnBuildButtonClicked()
    {
        if (house != null)
        {
            house.BuildHouse();
        }
    }
}
