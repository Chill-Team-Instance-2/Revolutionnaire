using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RV_ButtonTextColorHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI text;
    private Color defaultColor;
    [SerializeField] private float colorOffset = 50;
    private bool mouseOnButton = false;

    private void Awake()
    {
        defaultColor = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOnButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOnButton = false;
    }
    
    private void Update()
    {
        if (mouseOnButton)
        {
            text.color = Color.Lerp(text.color, defaultColor * 1.6f, 6 * Time.deltaTime);
        }
        else
        {
            text.color = Color.Lerp(text.color, defaultColor, 6 * Time.deltaTime);
        }
    }
}
