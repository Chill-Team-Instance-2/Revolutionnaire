using UnityEngine;

public class RV_AC_Card2 : RV_AC_Parent
{
    private RV_GameManager gameManager;
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
                if (gameManager.Turn%2==0)
                {
                    gameManager.InfluencePlayer += 5;
                }
                break;
            case 1:
                //TODO : faire carte commercant
                //currentcard.ReanableLostJet();
                 gameManager.InfluencePlayer -= 5;
                break;
            case 2:
                //TODO : faire carte intelligence
                /*
                 currentcard.ReanableLostJet();
                */
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
                break;
            case 2:
                break;
        }
    }
}
