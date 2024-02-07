using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RV_AC_Card5 : RV_AC_Parent
{
    private RV_GameManager gameManager;
    [SerializeField]private CardManager cardManager;
    [SerializeField] private Image card1Image;
    [SerializeField] private Image card2Image;
    [SerializeField] private Image card3Image;
    [SerializeField] private GameObject canvasChooseCard;
    public int cardIndex = 0;
    private List<GameObject> cards = new List<GameObject>();

    int whichTypeOfCard;
    int whichTypeOfCard2;
    int whichTypeOfCard3;
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
                gameManager.InfluencePlayer += 5;
                RV_ActionCard_Holder.Instance.DiscardCardInHand(this.gameObject);
                break;
            case 1:
                whichTypeOfCard = Random.Range(1, 2);
                whichTypeOfCard2 = Random.Range(1, 2);
                whichTypeOfCard3 = Random.Range(1, 2);
                if (whichTypeOfCard == 1)
                {
                    int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.ActionsCards.Count);
                    card1Image = RV_PickACardOnEndTour.Instance.ActionsCards[Cards].GetComponent<RV_ActionCard>().imageFront;
                    cards.Add(RV_PickACardOnEndTour.Instance.ActionsCards[Cards]);
                }
                if (whichTypeOfCard == 2)
                {
                    int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.RevoltsCards.Count);
                    card1Image = RV_PickACardOnEndTour.Instance.RevoltsCards[Cards].GetComponent<RV_ActionCard>().imageFront;
                    cards.Add(RV_PickACardOnEndTour.Instance.ActionsCards[Cards]);
                }                
                
                if (whichTypeOfCard2 == 1)
                {
                    int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.ActionsCards.Count);
                    card2Image = RV_PickACardOnEndTour.Instance.ActionsCards[Cards].GetComponent<RV_ActionCard>().imageFront;
                    cards.Add(RV_PickACardOnEndTour.Instance.ActionsCards[Cards]);
                }
                if (whichTypeOfCard2 == 2)
                {
                    int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.RevoltsCards.Count);
                    card2Image = RV_PickACardOnEndTour.Instance.RevoltsCards[Cards].GetComponent<RV_ActionCard>().imageFront;
                    cards.Add(RV_PickACardOnEndTour.Instance.ActionsCards[Cards]);
                }                
                
                if (whichTypeOfCard3 == 1)
                {
                    int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.ActionsCards.Count);
                    card3Image = RV_PickACardOnEndTour.Instance.ActionsCards[Cards].GetComponent<RV_ActionCard>().imageFront;
                    cards.Add(RV_PickACardOnEndTour.Instance.ActionsCards[Cards]);
                }
                if (whichTypeOfCard3 == 2)
                {
                    int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.RevoltsCards.Count);
                    card3Image = RV_PickACardOnEndTour.Instance.RevoltsCards[Cards].GetComponent<RV_ActionCard>().imageFront;
                    cards.Add(RV_PickACardOnEndTour.Instance.ActionsCards[Cards]);
                }
                RV_ActionCard_Holder.Instance.DiscardCardInHand(this.gameObject);
                canvasChooseCard.SetActive(true);
                break;
            case 2:
                if(cardManager.cards.Count >= 3)
                {
                    RV_DiceManager.Instance.ResultBonus += 5;
                }
                RV_ActionCard_Holder.Instance.DiscardCardInHand(this.gameObject);
                break;
            default:
                break;
        }
    }

    public void ChangeCardOrder()
    {
        RV_ActionCard_Holder.Instance.DiscardCardInHand(RV_PickACardOnEndTour.Instance.CurrentCard);
        RV_PickACardOnEndTour.Instance.animator = cards[cardIndex].GetComponent<Animator>();
        RV_PickACardOnEndTour.Instance.animator.SetTrigger("Flip");
        RV_PickACardOnEndTour.Instance.CurrentCard = cards[cardIndex];
        if (whichTypeOfCard == 1)
        {
            RV_PickACardOnEndTour.Instance.ActionsCards.Remove(cards[cardIndex]);
        }
        else if (whichTypeOfCard == 2)
        {
            RV_PickACardOnEndTour.Instance.RevoltsCards.Remove(cards[cardIndex]);
        }
        cards.Clear();
        canvasChooseCard.SetActive(false);
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
                RV_DiceManager.Instance.ResultBonus -= 5;
                break;
            default:
                break;
        }
    }
}
