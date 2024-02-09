using UnityEngine;

public class RV_AC_Card2 : RV_AC_Parent
{
    private RV_GameManager gameManager;
    private bool cardUsed = false;
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
    }

    public void Update()
    {
        if (cardUsed && IsActive)
        {
            RV_PickACardOnEndTour.Instance.CurrentCard.GetComponent<RV_RevoltCard>().ReanableLostJet();
            gameManager.InfluencePlayer -= 5;
        }
    }

    public override void OnReveal()
    {
        switch (RV_GameManager.Instance.PlayerTurn)
        {
            case 0:
                if (gameManager.Turn % 2 == 0)
                {
                    gameManager.InfluencePlayer += 5;
                }

                CanBePickup = false;
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
        
    }

    public void ActionCom()
    {
        GameObject currentCard = RV_PickACardOnEndTour.Instance.CurrentCard;
        if (currentCard.TryGetComponent<RV_RevoltCard>(out RV_RevoltCard revoltCard) && revoltCard.HasLostJet())
        {
            IsActive = true;
            revoltCard.ReanableLostJet();
            RV_GameManager.Instance.onendturn.AddListener(CheckEndTurnComm);
            gameManager.InfluencePlayer -= 5;
        }
    }

    public void ActionInt()
    {
        RV_PickACardOnEndTour.Instance.CurrentCard.GetComponent<RV_RevoltCard>().ReanableLostJet();
        RV_GameManager.Instance.PlayerTurn = 2;
        RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
    }

    public void CheckEndTurnComm()
    {
        RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
    }

    public override void OnDiscard()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 1:
                if (IsActive)
                {
                    IsActive = false;
                }
                break;
            case 2:
                break;
            default:
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
                cardUsed = false;
                break;
            case 2:
                break;
        }
    }
}
