using UnityEngine;

public class RV_AC_Card6 : RV_AC_Parent
{
    private RV_GameManager gameManager;
    //[SerializeField] private CardManager cardManager;
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
            case 2:
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
                RV_GameManager.Instance.onendturn.AddListener(CheckTurn);
                break;
            case 1:
                break;
            case 2:
                RV_GameManager.Instance.onendturn.AddListener(CheckTurn);
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
                    break;
                case 1:
                    RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
                    if (cardHolder.CardsInHandPlayer1.Count != 0)
                    {
                        cardHolder.DiscardCardInHand(cardHolder.CardsInHandPlayer1[0].gameObject);
                    }
                    else
                    {
                        RV_GameManager.Instance.InfluencePlayer -= 1;
                    }

                    if (cardHolder.CardsInHandPlayer2.Count != 0)
                    {
                        cardHolder.DiscardCardInHand(cardHolder.CardsInHandPlayer2[0].gameObject);
                    }
                    else
                        RV_GameManager.Instance.InfluencePlayer -= 1;

                    if (cardHolder.CardsInHandPlayer3.Count != 0)
                    {
                        cardHolder.DiscardCardInHand(cardHolder.CardsInHandPlayer3[0].gameObject);
                    }
                    else
                    {
                        RV_GameManager.Instance.InfluencePlayer -= 1;
                    }
                    break;
                case 2:
                    break;
            }
        }
    }

    public override void Action()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        if (cardHolder.IsCardInHand(transform))
        {      
            switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
            {
                case 0:
                    break;
                case 1:
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
                    ActionInt();
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
        //while (cardManager.cards.Count > 0)
        //{
        //    cardManager.cards.RemoveRange(cardManager.cards.Count, 1);
        //    MalusIndex--;
        //}
        //if (cardManager.cards.Count == 0 && MalusIndex > 0)
        //{
        //    gameManager.InfluencePlayer -= MalusIndex;
        //}
        //CardUsed = true;
        //break;
    }

    public void ActionInt()
    {
        if (!IsActive)
        {
            IsActive = true;
            RV_DiceManager.Instance.ResultBonus -= 8;
            RV_DiceManager.Instance.onDiceEnd.AddListener(CheckDiceEndMalus);
        }
    }

    public void CheckTurn()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                if (!IsActive)
                {
                    gameManager.InfluencePlayer -= 1;
                }
                break;
            case 1:
                break;
            case 2:
                if (!IsActive && gameManager.Turn > 15)
                {
                    gameManager.InfluencePlayer -= 20;
                    IsActive = true;
                }
                break;
        }
    }

    public void CheckDiceEndMalus()
    {
        if (IsActive)
        {
            RV_DiceManager.Instance.ResultBonus += 8;
            RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
            IsActive = false;
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
