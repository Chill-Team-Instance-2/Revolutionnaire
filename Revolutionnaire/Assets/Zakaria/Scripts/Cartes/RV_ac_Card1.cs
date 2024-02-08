using UnityEngine;
using UnityEngine.UI;

public class RV_AC_Card1 : RV_AC_Parent
{
    private RV_GameManager gameManager;
    private bool CardUsed = false;
    [SerializeField] private GameObject CanvaOddOrEven;    
    public int OddOrEven = 0; //0 = pair 1 = impair
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
    }
    public override void Action()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch(cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                if (RV_PickACardOnEndTour.Instance.CurrentCard.TryGetComponent<RV_RevoltCard>(out RV_RevoltCard revoltCard) && !IsActive)
                {
                    IsActive = true;
                    gameManager.Multiplier += 1;
                    RV_DiceManager.Instance.onDiceLaunch.AddListener(CheckDiceLaunch);
                }
                break;
            case 1:
                if (!IsActive)
                {
                    IsActive = true;
                    gameManager.InfluencePlayer -= 2;
                    RV_DiceManager.Instance.ResultBonus += 2;
                    RV_DiceManager.Instance.onDiceLaunch.AddListener(CheckDiceLaunch);
                }
                break;
            case 2:
                CanvaOddOrEven.SetActive(true);
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
                if (IsActive && RV_ActionCard_Holder.Instance.IsCardInHand(transform))
                {
                    IsActive = false;
                    gameManager.Multiplier -= 1;
                    RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
                }
                break;
            case 1:
                if (IsActive)
                {
                    IsActive = false;
                    RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
                }
                break;
        }
    }

    public void TakeEven()
    {
        OddOrEven = 0;
        OddOrEvenCheck();
        RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
    }

    public void TakeOdd()
    {
        OddOrEven = 1;
        OddOrEvenCheck();
        RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
    }

    public void OddOrEvenCheck()
    {
        RV_DiceManager.Instance.LaunchDice();
        if (CardUsed)
        {
            CanvaOddOrEven.SetActive(false);
            if ((OddOrEven == 0 && RV_DiceManager.Instance.DiceResult % 2 == 0) || (OddOrEven == 1 && RV_DiceManager.Instance.DiceResult % 2 != 0))
            {
                gameManager.InfluencePlayer += 6;
                CardUsed = false;
            }
            else
            {
                gameManager.InfluencePlayer -= 6;
                CardUsed = false;
            }
            CardUsed = false;
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
                RV_DiceManager.Instance.ResultBonus -= 2;
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}
