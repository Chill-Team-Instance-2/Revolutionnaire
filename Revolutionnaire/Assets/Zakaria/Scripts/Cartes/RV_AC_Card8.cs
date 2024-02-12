using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (cardHolder.IsCardInHand(transform))
        {
            switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
            {
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
                    ActionCom();
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

    }

    public void ActionCom()
    {
        if (!IsActive)
        {
            IsActive = true;
            RV_ActionCard_Holder.Instance.OnDiscard.AddListener(CheckDiscards);
        }
        
    }

    public void ActionInt()
    {
        if (!IsActive) 
        {
            gameManager.Multiplier -= 0.5f;
            IsActive = true;
            RV_DiceManager.Instance.ResultBonus += 200;
            RV_DiceManager.Instance.onDiceEnd.AddListener(CheckDiceLaunch);
        }
    }

    public override void OnReveal()
    {
        switch (RV_GameManager.Instance.PlayerTurn)
        {
            case 0:
                CanBePickup = false;
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

    public void CheckDiceLaunch()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 2:
                if (IsActive)
                {
                    gameManager.Multiplier += 0.5f;
                    RV_DiceManager.Instance.ResultBonus -= 200;
                    RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
                    IsActive = false;
                }
                break;
        }
    }

    public void CheckDiceEnd()
    {

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
                    discardList.Remove(actionCard.gameObject);
                    //discardList.RemoveAt(discardList.Count-1);
                    RV_ActionCard_Holder.Instance.StopAllCoroutines();
                    StartCoroutine(MoveCardToPioche(actionCard.transform));
                    RV_PickACardOnEndTour.Instance.ActionsCards.Add(actionCard.gameObject);
                }
                break;
        }
    }

    private IEnumerator MoveCardToPioche(Transform card)
    {
        card.GetComponent<Animator>().enabled = false;
        card.GetComponent<Animator>().ResetTrigger("FlipToDiscard");
        card.GetComponent<Animator>().ResetTrigger("Flip");
        card.GetComponent<Animator>().ResetTrigger("CurrentToPioche");
        card.GetComponent<Animator>().ResetTrigger("TDiscardToCurrent");

        Vector3 destination = RV_PickACardOnEndTour.Instance.baseCardPos;
        float timemove = 0;
        card.eulerAngles = Vector3.zero;
        while (timemove < 1)
        {
            card.position = Vector3.Lerp(card.position, destination, 40 * Time.deltaTime);
            timemove += Time.deltaTime;
            yield return null;
        }
        card.GetComponent<Animator>().enabled = true;
        yield return null;
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
