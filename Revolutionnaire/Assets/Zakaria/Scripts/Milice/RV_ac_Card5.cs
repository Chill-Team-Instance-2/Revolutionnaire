using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_ac_Card5 : MonoBehaviour
{
    private RV_GameManager gameManager;
    [SerializeField]private CardManager cardManager;
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
    }
    public void Action()
    {
        switch (gameManager.PlayersClass[gameManager.PlayerTurn])
        {
            case 0:
                gameManager.InfluencePlayer += 5;
                break;
            case 1:
                //TODO voir les 3 prochaines cartes et choisir
                break;
            case 2:
                if(cardManager.cards.Count >= 3)
                {
                    //diceManager.diceResult += 5;
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
