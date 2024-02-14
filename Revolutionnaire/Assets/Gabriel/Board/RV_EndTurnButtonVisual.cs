using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RV_EndTurnButtonVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textEnd;
    private Color baseColor;
    private bool oldCanEndTurn = true;
    private RV_GameManager gameManager;

    private void Awake()
    {
        gameManager = RV_GameManager.Instance;
        baseColor = textEnd.color;
    }

    private void Update()
    {
        if (!gameManager.CanEndTurn)
        {
            textEnd.color = new Color(0.1f, 0.1f, 0.1f);
        }
        if (!oldCanEndTurn && gameManager.CanEndTurn)
        {
            textEnd.color = baseColor;
        }
        oldCanEndTurn = gameManager.CanEndTurn;
    }
}
