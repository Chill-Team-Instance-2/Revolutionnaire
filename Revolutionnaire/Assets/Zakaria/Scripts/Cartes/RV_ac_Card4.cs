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
        if (cardHolder.IsCardInHand(transform))
        {
            switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
            {
                case 0:
                    ActionMil();
                    break;
                case 1:
                    ActionCom();
                    break;
                case 2:
                    ActionInt();
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (gameManager.PlayerTurn)
            {
                case 0:
                    ActionMil();
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

    public void ActionMil()
    {
        if (!IsActive)
        {
            IsActive = true;
            gameManager.Multiplier += 1;
            RV_DiceManager.Instance.onDiceEnd.AddListener(CheckDiceLaunch);
        }
    }

    public void ActionCom()
    {
        RV_PickACardOnEndTour pioche = RV_PickACardOnEndTour.Instance;
        if (pioche.DiscardsList.Count >= 1)
        {
            pioche.ActualToDiscard();
            pioche.DiscardToActual(pioche.DiscardsList[pioche.DiscardsList.Count - 2]);
            RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
        }
    }

    public void ActionInt()
    {
        RV_DiceManager.Instance.LaunchDice();
        if (RV_DiceManager.Instance.DiceResult >= 15)
        {
            gameManager.InfluencePlayer += 15;
        }
        else
        {
            gameManager.InfluencePlayer -= 10;
        }
        RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
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
