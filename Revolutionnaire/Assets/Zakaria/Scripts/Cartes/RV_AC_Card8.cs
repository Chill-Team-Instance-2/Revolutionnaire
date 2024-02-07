using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AC_Card8 : RV_AC_Parent
{
    private RV_GameManager gameManager;
    private bool CardUsed = false;
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
    }
    public override void Action()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                gameManager.InfluencePlayer += 5;
                gameManager.Turn += 1;
                break;
            case 1:
                //TODO : garder carte dans la pioche pendant un tour
                break;
            case 2:
                RV_DiceManager.Instance.ResultBonus =+ 200;
                gameManager.Multiplier = 0.5f;
                break;
            default:
                break;
        }
    }
    public override void EndAction()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                gameManager.Multiplier = 1;
                break;
            default:
                break;
        }
    }
}