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
    public override void Action()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                CardUsed = true;
                Turn = 0;
                gameManager.Multiplier -= 0.5f;
                RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
                RV_GameManager.Instance.onendturn.AddListener(CheckTurn);
                break;
            case 1:
                RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
                gameManager.PickACardOnEndTour.ActualToDiscard();
                gameManager.PickACardOnEndTour.PickACard();
                //TODO : Redraw (anim)
                break;
            case 2:
                RV_DiceManager.Instance.DiceResult = RV_DiceManager.Instance.DiceResult * 2;
                RV_ActionCard_Holder.Instance.DiscardCardInHand(this.gameObject);
                break;
            default:
                break;
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
