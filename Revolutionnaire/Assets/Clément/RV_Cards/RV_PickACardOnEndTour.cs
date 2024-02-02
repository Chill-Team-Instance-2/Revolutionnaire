using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RV_PickACardOnEndTour : MonoBehaviour
{
    public List<GameObject> ActionsCards = new List<GameObject>();
    public List<GameObject> RevoltsCards = new List<GameObject>();
    public List<GameObject> DiscardsList = new List<GameObject>();

    private Animator animator;

 
    public void PickACard()
    {
        int wichTypeOfCard = Random.Range(1, 2);
        if(wichTypeOfCard == 1)
        {
            int Cards = Random.Range(0, ActionsCards.Count);
            animator = ActionsCards[Cards].GetComponent<Animator>();
            animator.SetTrigger("Flip");
        }
    }

}

