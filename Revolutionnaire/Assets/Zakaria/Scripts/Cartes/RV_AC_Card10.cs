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
    }

    public override void OnReveal()
    {
        switch (RV_GameManager.Instance.PlayerTurn)
        {
            case 0:
                break;
            case 1:
                CanBePickup = false;
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
                gameManager.onendturn.AddListener(CheckTurn);
                break;
            case 1:
                break;
            case 2:
                turnCount = 0;
                IsActive = true;
                gameManager.onendturn.AddListener(CheckTurn);
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
                if (!IsActive)
                {
                    IsActive = true;
                    BackWardTurn();
                }
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    public override void OnDiscard()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                break;
            case 1:
                IsActive = false;
                break;
            case 2:
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
                if (gameManager.PlayerTurn == 0 && RV_PickACardOnEndTour.Instance.CurrentCard.TryGetComponent<RV_RevoltCard>(out RV_RevoltCard revolt) && IsActive)
                {
                    gameManager.DisableEndTurn();
                    RV_PickACardOnEndTour.Instance.IsReanablingTurn = false;
                    RV_DiceManager.Instance.onDiceEnd.AddListener(CheckDiceLaunch);
                }
                break;
            case 2:
                if (turnCount != -1 && IsActive)
                {
                    if (RV_GameManager.Instance.PlayerTurn == 2 && turnCount > 0)
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
