using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AC_Card9 : MonoBehaviour
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
                //jeter dé
                //int result = diceManager.diceResult;
                //result = ((int)System.Math.Floor(result/2));
                //gameManager.InfluencePlayer = result;
                break;
            case 1:
                //TODO : voir les 3 prochaines puis choisir
                break;
            case 2:
                /*
                 if(diceManager.diceResult%3==0) 
                {
                    gameManager.adding += 3;
                }
                 */
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
