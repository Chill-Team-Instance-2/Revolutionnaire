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
                gameManager.Multiplier = gameManager.Multiplier + 1;
                print(gameManager.Multiplier);
                RV_ActionCard_Holder.Instance.DiscardCardInHand(this.gameObject);
                break;
            case 1:
                gameManager.InfluencePlayer -= 2;
                RV_DiceManager.Instance.ResultBonus += 2;
                print(gameManager.InfluencePlayer);
                RV_ActionCard_Holder.Instance.DiscardCardInHand(this.gameObject);
                break;
            case 2:
                CanvaOddOrEven.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void TakeEven()
    {
        OddOrEven = 0;
        CardUsed = true;
    }

    public void TakeOdd()
    {
        OddOrEven = 1;
        CardUsed = true;
    }

    public void OddOrEvenCheck()
    {
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
        }
    }

    public override void EndAction()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                gameManager.Multiplier = gameManager.Multiplier - 1;
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
