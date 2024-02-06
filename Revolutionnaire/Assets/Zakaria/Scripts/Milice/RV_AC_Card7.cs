using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AC_Card7 : MonoBehaviour
{
    private RV_GameManager gameManager;
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
                gameManager.Bonus += 1;
                break;
            case 1:
                CardUsed = true;
                break;
            case 2:
                gameManager.Turn -= 1;
                break;
            default:
                break;
        }
    }

    public void AddWhenEndTurn()
    {
        if (CardUsed)
        {
            gameManager.InfluencePlayer += 1;
        }
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