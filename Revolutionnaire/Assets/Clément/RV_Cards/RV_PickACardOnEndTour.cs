using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RV_PickACardOnEndTour : MonoBehaviour
{
    public List<GameObject> ActionsCards;
    public List<GameObject> RevoltsCards;
    public List<GameObject> DiscardsList;

    private Animator animator;

    public void PickACard()
    {
        int wichTypeOfCard = Random.Range(1, 3);
        Debug.Log(wichTypeOfCard);

        if (wichTypeOfCard == 1 && ActionsCards.Count > 0)
        {
            int Cards = Random.Range(0, ActionsCards.Count);
            animator = ActionsCards[Cards].GetComponent<Animator>();
            animator.SetTrigger("Flip");
            DiscardsList.Add(ActionsCards[Cards]);
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
            DiscardsList.Add(RevoltsCards[Cards]);
            RevoltsCards.Remove(RevoltsCards[Cards]);
        }
        else if (RevoltsCards.Count == 0)
        {
            wichTypeOfCard = 1;
        }
    }

    public void ActualToDiscard()
    {
        DiscardsList[DiscardsList.Count-2].GetComponent<Animator>().SetTrigger("FlipToDiscard");
    }

}



