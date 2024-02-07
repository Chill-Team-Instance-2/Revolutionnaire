using UnityEngine;

public class RV_AC_Card6 : RV_AC_Parent
{
    private RV_GameManager gameManager;
    private bool CardUsed = false;
    [SerializeField] private CardManager cardManager;
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
        Action();
    }
    public override void Action()
    {
        int MalusIndex = 3;
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                gameManager.Bonus = -1;
                CardUsed = true;
                break;
            case 1:
                while (cardManager.cards.Count > 0)
                {
                    cardManager.cards.RemoveRange(cardManager.cards.Count, 1);
                    MalusIndex--;
                }
                if (cardManager.cards.Count == 0 && MalusIndex > 0)
                {
                    gameManager.InfluencePlayer -= MalusIndex;
                }
                CardUsed = true;
                break;
            case 2:
                CheckTurn();
                break;
            default:
                break;
        }
    }

    public void CheckTurn()
    {
            if (!CardUsed && gameManager.Turn > 15)
            {
                gameManager.InfluencePlayer -= 20;
            }
            else
            {
                gameManager.InfluencePlayer -= 8;
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
