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
                //TODO : réfléchir à comment faire pour qu'à chaque défaussement les points augmentent
                break;
            case 2:
                gameManager.Turn -= 1;
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
