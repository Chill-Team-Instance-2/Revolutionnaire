using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RV_AC_Card9 : RV_AC_Parent
{
    private RV_GameManager gameManager;
    private bool CardUsed = false;

    private Texture2D card1Texture;
    private Texture2D card2Texture;
    private Texture2D card3Texture;

    [SerializeField] private GameObject canvasChooseCard;

    public int cardIndex = 0;
    private List<GameObject> cards = new List<GameObject>();

    [SerializeField] private Image card1Image;
    [SerializeField] private Image card2Image;
    [SerializeField] private Image card3Image;

    int whichTypeOfCard;
    int whichTypeOfCard2;
    int whichTypeOfCard3;
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
    }

    public override void OnReveal()
    {
        switch (RV_GameManager.Instance.PlayerTurn)
        {
            case 0:
                gameManager.DisableEndTurn();
                CanBePickup = false;
                gameManager.Invoke("EnableEndTurn", RV_DiceManager.Instance.DiceTime);
                break;
        }
    }

    public override void OnFinishedReveal()
    {
        switch (RV_GameManager.Instance.PlayerTurn)
        {
            case 0:
                gameManager.AddInfluenceWithDelay(RV_DiceManager.Instance.LaunchDice() / 2, RV_DiceManager.Instance.DiceTime);
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
        StartCoroutine(LaunchDiceComm());
        double result = RV_DiceManager.Instance.DiceResult;
        result = System.Math.Floor(result / 2);
        gameManager.InfluencePlayer = ((int)result);
        RV_PickACardOnEndTour.Instance.ActualToDiscard();
        RV_ActionCard_Holder.Instance.DiscardCardInHand(gameObject);
        RV_PickACardOnEndTour.Instance.PickACard();
    }

    public void ActionCom()
    {
        whichTypeOfCard = Random.Range(1, 2);
        whichTypeOfCard2 = Random.Range(1, 2);
        whichTypeOfCard3 = Random.Range(1, 2);
        if (whichTypeOfCard == 1)
        {
            int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.ActionsCards.Count);
            card1Texture = RV_PickACardOnEndTour.Instance.ActionsCards[Cards].GetComponent<RV_ActionCard>().spriteFront.texture;
            cards.Add(RV_PickACardOnEndTour.Instance.ActionsCards[Cards]);
        }
        if (whichTypeOfCard == 2)
        {
            int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.RevoltsCards.Count);
            card1Texture = RV_PickACardOnEndTour.Instance.RevoltsCards[Cards].GetComponent<RV_RevoltCard>().spriteFront.texture;
            cards.Add(RV_PickACardOnEndTour.Instance.RevoltsCards[Cards]);
        }

        if (whichTypeOfCard2 == 1)
        {
            int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.ActionsCards.Count);
            card2Texture = RV_PickACardOnEndTour.Instance.ActionsCards[Cards].GetComponent<RV_ActionCard>().spriteFront.texture;
            cards.Add(RV_PickACardOnEndTour.Instance.ActionsCards[Cards]);
        }
        if (whichTypeOfCard2 == 2)
        {
            int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.RevoltsCards.Count);
            card2Texture = RV_PickACardOnEndTour.Instance.RevoltsCards[Cards].GetComponent<RV_RevoltCard>().spriteFront.texture;
            cards.Add(RV_PickACardOnEndTour.Instance.RevoltsCards[Cards]);
        }

        if (whichTypeOfCard3 == 1)
        {
            int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.ActionsCards.Count);
            card3Texture = RV_PickACardOnEndTour.Instance.ActionsCards[Cards].GetComponent<RV_ActionCard>().spriteFront.texture;
            cards.Add(RV_PickACardOnEndTour.Instance.ActionsCards[Cards]);
        }
        if (whichTypeOfCard3 == 2)
        {
            int Cards = Random.Range(0, RV_PickACardOnEndTour.Instance.RevoltsCards.Count);
            card3Texture = RV_PickACardOnEndTour.Instance.RevoltsCards[Cards].GetComponent<RV_RevoltCard>().spriteFront.texture;
            cards.Add(RV_PickACardOnEndTour.Instance.RevoltsCards[Cards]);
        }
        RV_ActionCard_Holder.Instance.DiscardCardInHand(this.gameObject);
        canvasChooseCard.SetActive(true);
    }

    public void ActionInt()
    {
        if (RV_DiceManager.Instance.DiceResult % 3 == 0)
        {
            gameManager.Bonus += 3;
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

    private IEnumerator LaunchDiceComm()
    {
        int result = RV_DiceManager.Instance.LaunchDice();
        result = ((int)System.Math.Floor(result / 2f));

        yield return new WaitForSeconds(2);

        RV_GameManager.Instance.InfluencePlayer += result;
        yield return null;
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
                if(gameManager.Bonus > 0)
                {
                    gameManager.Bonus -= 3;
                }
                break;
            default:
                break;
        }
    }
}
