using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RV_AC_Card10 : RV_AC_Parent
{
    private RV_GameManager gameManager;
    [SerializeField] Button TurnButton;
    private bool CardUsed = false;

    int turnCount = -1;

    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
        gameManager.onendturn.AddListener(CheckTurn);
    }

    public override void OnReveal()
    {
        switch (RV_GameManager.Instance.PlayerTurn)
        {
            case 0:
                break;
        }
    }

    public override void OnPickUp()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                IsActive = true;
                print(IsActive);
                gameManager.DisableEndTurn();
                break;
            case 1:
                break;
            case 2:
                turnCount = 0;
                IsActive = true;
                break;
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
                BackWardTurn();
                RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
                break;
            case 2:
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
                RV_GameManager.Instance.EnableEndTurn();
                IsActive = false;
                cardHolder.DiscardCardInHand(gameObject);
                break;
        }
    }

    public void CheckTurn()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                if (RV_PickACardOnEndTour.Instance.CurrentCard.TryGetComponent<RV_RevoltCard>(out RV_RevoltCard revolt) && IsActive)
                {
                    RV_DiceManager.Instance.onDiceEnd.AddListener(CheckDiceLaunch);
                }
                break;
            case 2:
                if (turnCount != -1 && IsActive)
                {
                    if (RV_GameManager.Instance.PlayerTurn == 0 && turnCount > 0)
                    {
                        RV_GameManager.Instance.EndTurn();
                    }
                    turnCount++;
                    IsActive = false;
                    RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
                }
                break;
        }
    }

    public void BackWardTurn()
    {
        gameManager.Turn -= 1;
        gameManager.InfluencePlayer -= 10;
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
