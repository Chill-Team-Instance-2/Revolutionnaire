using UnityEngine;

public class RV_ac_Card3 : RV_AC_Parent
{
    private RV_GameManager gameManager;
    private int Turn;
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
        if (!IsActive)
        {
            CanBePickup = true;
            IsActive = true;
            CardUsed = true;
            Turn = 0;
            gameManager.Multiplier -= 0.5f;
            //RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
        }
    }

    public void ActionCom()
    {
        if (RV_ActionCard_Holder.Instance.IsCardInHand(transform))
            RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
        RV_PickACardOnEndTour.Instance.ActualToDiscard();
        RV_PickACardOnEndTour.Instance.PickACard();
        //TODO : Redraw (anim)
    }

    public void ActionInt()
    {
        if (!IsActive)
        {
            RV_DiceManager.Instance.ResultMultiplier += 1;
            RV_DiceManager.Instance.onDiceEnd.AddListener(CheckDice);
            IsActive = true;
        }
    }

    public void CheckDice()
    {
        if (IsActive)
        {
            RV_DiceManager.Instance.ResultMultiplier -= 1;
            RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
            IsActive = false;
        }
    }
    public void CheckTurn()
    {
        if (CardUsed)
        {
            if (Turn == 5)
            {
                gameManager.Multiplier += 0.5f;
                gameManager.Multiplier += 1;
            }
            else if (Turn == 10)
            {
                gameManager.Multiplier -= 1;
                RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
                IsActive = false;
            }
            Turn++;
        }
    }
    public override void EndAction()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                CardUsed = false;
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
