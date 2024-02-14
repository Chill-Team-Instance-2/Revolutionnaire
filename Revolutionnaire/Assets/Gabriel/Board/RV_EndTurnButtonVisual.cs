using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RV_EndTurnButtonVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI textEnd;
    private Color baseColor;
    private bool oldCanEndTurn = true;
    private RV_GameManager gameManager;

    private bool mouseOnButton = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f) * 1.7f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOnButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOnButton = false;
    }

    private void Awake()
    {
        baseColor = textEnd.color;
    }

    private void Start()
    {
        gameManager = RV_GameManager.Instance;
    }

    private void Update()
    {
        if (mouseOnButton)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f) * 2f, 8 * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f) * 1.8f, 8 * Time.deltaTime);
        }
        //if (!gameManager.CanEndTurn)
        //{
        //    textEnd.color = new Color(0.1f, 0.1f, 0.1f);
        //}
        //if (!oldCanEndTurn && gameManager.CanEndTurn)
        //{
        //    textEnd.color = baseColor;
        //}
        //oldCanEndTurn = gameManager.CanEndTurn;
    }
}
