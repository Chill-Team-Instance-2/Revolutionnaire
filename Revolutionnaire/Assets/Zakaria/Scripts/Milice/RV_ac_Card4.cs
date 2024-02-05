using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AC_Card4 : MonoBehaviour
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
                gameManager.Multiplier += 1;
                break;
            case 1:
                //TODO : remettre la carte de la fausse dans la pioche
                break;
            case 2:
                if(RV_DiceManager.Instance.DiceResult <= 15) 
                {
                    gameManager.InfluencePlayer += 15;
                }
                else 
                {
                    gameManager.InfluencePlayer -= 10;
                }
                break;
            default:
                break;
        }
    }
    public void EndAction()
    {
        switch (gameManager.PlayersClass[gameManager.PlayerTurn])
        {
            case 0:
                gameManager.Multiplier -= 1;
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
