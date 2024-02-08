using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AC_Card7 : RV_AC_Parent
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
                break;
            case 1:
                break;
            case 2:
                CanBePickup = false;
                break;
        }
    }

    public override void OnPickUp()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 1:
                IsActive = true;
                RV_ActionCard_Holder.Instance.OnDiscard.AddListener(CheckDiscards);
                break;
        }
    }

    public void CheckDiscards()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 1:
                if (IsActive)
                {
                    RV_GameManager.Instance.InfluencePlayer += 1;
                }
                break;
        }
    }

    public override void OnDiscard()
    {
        switch (RV_GameManager.Instance.PlayerTurn)
        {
            case 0:
                IsActive = false;
                break;
            case 1:
                IsActive = false;
                break;
            case 2:
                RV_GameManager.Instance.Turn--;
                break;
        }
    }

    public override void Action()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                RV_GameManager.Instance.onendturn.AddListener(AddWhenEndTurn);
                IsActive = true;
                break;
            case 1:
                CardUsed = true;
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    public void AddWhenEndTurn()
    {
        if (IsActive)
        {
            gameManager.InfluencePlayer += 1;
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
