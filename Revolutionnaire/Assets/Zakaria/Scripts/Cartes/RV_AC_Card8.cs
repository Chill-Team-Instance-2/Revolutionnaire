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


    public override void OnReveal()
    {
        switch (RV_GameManager.Instance.PlayerTurn)
        {
            case 0:
                CanBePickup = false;
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    public override void OnDiscard()
    {
        if (CanBePickup)
        {

        }
        else
        {
            switch (RV_GameManager.Instance.PlayerTurn)
            {
                case 0:
                    gameManager.InfluencePlayer += 5;
                    gameManager.Turn += 1;
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }

    public override void Action()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                break;
            case 1:
                IsActive = true;
                RV_ActionCard_Holder.Instance.OnDiscard.AddListener(CheckDiscards);
                break;
            case 2:
                RV_DiceManager.Instance.ResultBonus += 200;
                gameManager.Multiplier -= 0.5f;
                RV_DiceManager.Instance.onDiceLaunch.AddListener(CheckDiceLaunch);
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
            case 2:
                gameManager.Multiplier += 0.5f;
                RV_DiceManager.Instance.ResultBonus -= 200;
                break;
        }
    }

    public void CheckDiscards()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 1:
                List<GameObject> discardList = RV_PickACardOnEndTour.Instance.DiscardsList;
                if (discardList[discardList.Count-1].TryGetComponent<RV_ActionCard>(out RV_ActionCard actionCard))
                {
                    discardList.RemoveAt(discardList.Count-1);
                    RV_PickACardOnEndTour.Instance.ActionsCards.Add(actionCard.gameObject);
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
