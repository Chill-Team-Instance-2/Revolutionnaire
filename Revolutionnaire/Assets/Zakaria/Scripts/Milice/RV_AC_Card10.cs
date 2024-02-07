using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RV_AC_Card10 : RV_AC_Parent
{
    private RV_GameManager gameManager;
    [SerializeField] Canvas CanvasBackwardTurn;
    [SerializeField] Button TurnButton;
    private bool CardUsed = false;

    int turnCount = -1;

    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
    }

    public override void OnReveal()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                gameManager.DisableEndTurn();
                CanBePickup = false;
                gameManager.AddInfluenceWithDelay(RV_DiceManager.Instance.LaunchDice(), RV_DiceManager.Instance.DiceTime);
                gameManager.Invoke("EnableEndTurn", RV_DiceManager.Instance.DiceTime);
                break;
        }
    }

    public override void OnPickUp()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                break;
            case 2:
                turnCount = 0;
                break;
        }
    }

    public override void Action()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                /* 
                TODO : chercher carte actuelle et défausser si c'est une action
                Trouver carte actuelle
                gameManager.PickACardOnEndTour.PickACard();
                gameManager.PickACardOnEndTour.ActualToDiscard();
                */

                
                break;
            case 1:
                CanvasBackwardTurn.enabled = true;
                break;
            case 2:
                gameManager.EndTurn();
                break;
            default:
                break;
        }
    }

    public void CheckTurn()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 2:
                if (turnCount != -1)
                {
                    if (RV_GameManager.Instance.PlayerTurn == 0 && turnCount > 0)
                    {
                        RV_GameManager.Instance.EndTurn();
                    }
                    turnCount++;
                }
                break;
        }
    }

    public void BackWardTurn()
    {
        gameManager.Turn -= 1;
        gameManager.InfluencePlayer -= 10;
        CanvasBackwardTurn.enabled = false;
    }
    public override void EndAction()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                TurnButton.enabled = true;
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
