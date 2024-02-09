using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AC_Card4 : RV_AC_Parent
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
                if (!IsActive)
                {
                    IsActive = true;
                    gameManager.Multiplier += 1;
                    RV_DiceManager.Instance.onDiceEnd.AddListener(CheckDiceLaunch);
                }
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

    public void CheckDiceLaunch()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                if (IsActive)
                {
                    IsActive = false;
                    gameManager.Multiplier -= 1;
                    if (!RV_RevoltCard_Manager.Instance.LastTrySuccesResult)
                    {
                        RV_GameManager.Instance.InfluencePlayer -= RV_RevoltCard_Manager.Instance.LastTryInfluenceGain;
                    }
                    RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
                }
                break;
        }
    }

    public override void EndAction()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
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
