using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AC_Card5 : MonoBehaviour
{
    private RV_GameManager gameManager;
    [SerializeField]private CardManager cardManager;
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
    }
    public void Action()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                gameManager.InfluencePlayer += 5;
                break;
            case 1:
                //TODO voir les 3 prochaines cartes et choisir
                break;
            case 2:
                if(cardManager.cards.Count >= 3)
                {
                    RV_DiceManager.Instance.ResultBonus += 5;
                }
                break;
            default:
                break;
        }
    }
    public void EndAction()
    {
        RV_ActionCard_Holder cardHolder = RV_ActionCard_Holder.Instance;
        switch (cardHolder.GetPlayerFromList(cardHolder.GetListOfCard(transform)))
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                RV_DiceManager.Instance.ResultBonus -= 5;
                break;
            default:
                break;
        }
    }
}
