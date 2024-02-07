using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RV_PickACardOnEndTour : MonoBehaviour
{
    public static RV_PickACardOnEndTour Instance;

    public List<GameObject> ActionsCards;
    public List<GameObject> RevoltsCards;
    public List<GameObject> DiscardsList;

    public GameObject CurrentCard;

    private Animator animator;

    private void Awake()
    {
        Instance = this;
    }

    public void PickACard()
    {
        int wichTypeOfCard = Random.Range(1, 3);
        Debug.Log(wichTypeOfCard);

        if (wichTypeOfCard == 1 && ActionsCards.Count > 0)
        {
            int Cards = Random.Range(0, ActionsCards.Count);
            animator = ActionsCards[Cards].GetComponent<Animator>();
            animator.SetTrigger("Flip");
            //DiscardsList.Add(ActionsCards[Cards]);
            CurrentCard = ActionsCards[Cards];
            ActionsCards.Remove(ActionsCards[Cards]);
        }
        else if (ActionsCards.Count == 0)
        {
            wichTypeOfCard = 2;
        }

        if (wichTypeOfCard == 2 && RevoltsCards.Count > 0)
        {
            int Cards = Random.Range(0, RevoltsCards.Count);
            animator = RevoltsCards[Cards].GetComponent<Animator>();
            animator.SetTrigger("Flip");
            //DiscardsList.Add(RevoltsCards[Cards]);
            CurrentCard = RevoltsCards[Cards];
            RevoltsCards.Remove(RevoltsCards[Cards]);
        }
        else if (RevoltsCards.Count == 0)
        {
            wichTypeOfCard = 1;
        }
    }

    public void ActualToDiscard()
    {
        if (CurrentCard)
        {
            if (CurrentCard.TryGetComponent<RV_RevoltCard>(out RV_RevoltCard revolt)) //if revolt card
            {
                CurrentCard.GetComponent<Animator>().SetTrigger("FlipToDiscard");
                DiscardsList.Add(CurrentCard);
            }
            else //if action card
            {
                CurrentCard.GetComponent<Animator>().enabled = false;
                RV_ActionCard_Holder.Instance.PutCardInHand(CurrentCard, RV_GameManager.Instance.PlayerTurn);
                CurrentCard = null;
            }
        }
    }
}



