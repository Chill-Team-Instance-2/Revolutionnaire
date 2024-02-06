using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_ActionCard_Holder : MonoBehaviour
{
    public static RV_ActionCard_Holder Instance;

    [SerializeField] public Transform HandPlayer1; 
    [SerializeField] public Transform HandPlayer2; 
    [SerializeField] public Transform HandPlayer3;

    public List<Transform> CardsInHandPlayer1 = new List<Transform>();
    public List<Transform> CardsInHandPlayer2 = new List<Transform>();
    public List<Transform> CardsInHandPlayer3 = new List<Transform>();

    [SerializeField] public Transform DiscardTransform;

    private void Awake()
    {
        Instance = this;
    }

    public void PutCardInHand(GameObject card, int playerId)
    {
        switch (playerId)
        {
            case 0:
                StartCoroutine(AnimationPlaceCardInHand(card.transform, HandPlayer1.position));
                CardsInHandPlayer1.Add(card.transform);
                break;
            case 1:
                StartCoroutine(AnimationPlaceCardInHand(card.transform, HandPlayer2.position));
                CardsInHandPlayer2.Add(card.transform);
                break;
            case 2:
                StartCoroutine(AnimationPlaceCardInHand(card.transform, HandPlayer3.position));
                CardsInHandPlayer3.Add(card.transform);
                break;
        }
        
    }

    public void FocusOnCardInHand(GameObject card)
    {
        //animation
    }

    public void UnfocusOnCardInHand(GameObject card)
    {
        //undoAnimation
    }

    public void DiscardCardInHand(GameObject card)
    {
        StartCoroutine(AnimationPlaceCardInHand(card.transform, DiscardTransform.position));
        RV_PickACardOnEndTour.Instance.DiscardsList.Add(card);
        GetListOfCard(card.transform).Remove(card.transform);
    }

    private IEnumerator AnimationPlaceCardInHand(Transform card, Vector3 destination)
    {
        float timer = 0;
        while (timer < 2)
        {
            card.position = Vector3.Lerp(card.position, destination, 10 * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    public List<Transform> GetListOfCard(Transform card)
    {
        if (CardsInHandPlayer1.Contains(card))
            return CardsInHandPlayer1;
        if (CardsInHandPlayer2.Contains(card))
            return CardsInHandPlayer2;
        if (CardsInHandPlayer3.Contains(card))
            return CardsInHandPlayer3;

        return null;
    }

    public bool CardIsInHand(Transform card)
    {
        if (CardsInHandPlayer1.Contains(card))
            return true;
        if (CardsInHandPlayer2.Contains(card))
            return true;
        if (CardsInHandPlayer3.Contains(card))
            return true;
        
        return false;
    }
}
