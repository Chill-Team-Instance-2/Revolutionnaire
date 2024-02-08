using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RV_PlayerTurns_Visual : MonoBehaviour
{
    private RV_GameManager gameManager;

    [SerializeField] private Image milPortrait;
    [SerializeField] private Image comPortrait;
    [SerializeField] private Image intPortrait;

    private void Start()
    {
        gameManager = RV_GameManager.Instance;
        gameManager.onendturn.AddListener(UpdatePortraits);
        gameManager.OnChangePlayer.AddListener(UpdatePortraits);
        UpdatePortraits();
    }

    public void UpdatePortraits()
    {
      switch (gameManager.PlayerTurn)
        {
            case 0:
                milPortrait.color = new Color(1, 1, 1);
                comPortrait.color = new Color(0.5f, 0.5f, 0.5f);
                intPortrait.color = new Color(0.5f, 0.5f, 0.5f);
                break;
            case 1:
                milPortrait.color = new Color(0.5f, 0.5f, 0.5f);
                comPortrait.color = new Color(1, 1, 1);
                intPortrait.color = new Color(0.5f, 0.5f, 0.5f);
                break;
            case 2:
                milPortrait.color = new Color(0.5f, 0.5f, 0.5f);
                comPortrait.color = new Color(0.5f, 0.5f, 0.5f);
                intPortrait.color = new Color(1, 1, 1);
                break;  
        }
    }
}
