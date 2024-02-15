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

    public override void OnDiscard()
    {
        base.OnDiscard();
    }

    public void ActionMil()
    {
        if (RV_PickACardOnEndTour.Instance.CurrentCard.TryGetComponent<RV_RevoltCard>(out RV_RevoltCard revoltCard) && !IsActive)
        {
            IsActive = true;
            gameManager.Multiplier += 1;
            RV_DiceManager.Instance.onDiceEnd.AddListener(CheckDiceLaunch);
        }
    }

    public override void OnReveal()
    {
        switch (RV_GameManager.Instance.PlayerTurn)
        {
            case 2:
                CanBePickup = false;
                CanvaOddOrEven.SetActive(true);
                break;
        }
    }

    public void ActionCom()
    {
        if (!IsActive)
        {
            IsActive = true;
            gameManager.InfluencePlayer -= 2;
            RV_DiceManager.Instance.ResultBonus += 2;
            RV_DiceManager.Instance.onDiceEnd.AddListener(CheckDiceLaunchCom);
        }
    }

    public void CheckDiceLaunchCom()
    {
        if (IsActive)
        {
            IsActive = false;
            RV_DiceManager.Instance.ResultBonus -= 2;
            RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
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
                break;
        }
    }

    public void TakeEven()
    {
        CanvaOddOrEven.SetActive(false);
        OddOrEven = 0;
        OddOrEvenCheck();
    }

    public void TakeOdd()
    {
        CanvaOddOrEven.SetActive(false);
        OddOrEven = 1;
        OddOrEvenCheck();
    }

    public void OddOrEvenCheck()
    {
        RV_DiceManager.Instance.LaunchDice();
        CanvaOddOrEven.SetActive(false);
        if ((OddOrEven == 0 && RV_DiceManager.Instance.DiceResult % 2 == 0) || (OddOrEven == 1 && RV_DiceManager.Instance.DiceResult % 2 != 0))
        {
            gameManager.AddInfluenceWithDelay(6, RV_DiceManager.Instance.DiceTime);
        }
        else
        {
            gameManager.AddInfluenceWithDelay(-6, RV_DiceManager.Instance.DiceTime);
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
