using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AC_Card10 : MonoBehaviour
{
    private RV_GameManager gameManager;
    [SerializeField] Canvas CanvasBackwardTurn;
    private bool CardUsed = false;
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
    }
    public void Action()
    {
        switch (gameManager.PlayersClass[gameManager.PlayerTurn])
        {
            case 0:
                //TODO ; Forcer le jet de dé
                break;
            case 1:
                CanvasBackwardTurn.enabled = true;
                break;
            case 2:
                //TODO : forcer le fait de passer le tour si c'est les intellectuels
                break;
            default:
                break;
        }
    }

    public void BackWardTurn()
    {
        gameManager.Turn -= 1;
        gameManager.InfluencePlayer -= 10;
        CanvasBackwardTurn.enabled = false;
    }
    public void EndAction()
    {
        switch (gameManager.PlayersClass[gameManager.PlayerTurn])
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}
