using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class RV_Highlight_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI buttonText;
    private string savedString;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.text = "-" + savedString + "-";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.text = savedString;
    }

    private void Awake()
    {
        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();   
        savedString = buttonText.text;
    }
}
