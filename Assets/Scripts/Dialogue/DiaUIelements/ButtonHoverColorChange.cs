using UnityEngine;
using TMPro;
using UnityEngine.EventSystems; // Required for the event data

public class ButtonHoverColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.red;

    private void Start()
    {
        // Set initial color
        buttonText.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change to hover color
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Change back to normal color
        buttonText.color = normalColor;
    }
}
