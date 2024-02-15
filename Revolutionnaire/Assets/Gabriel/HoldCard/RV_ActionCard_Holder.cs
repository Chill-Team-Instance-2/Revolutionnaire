using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent OnDiscard;

    private void Awake()
    {
        Instance = this;
    }

    public void PutCardInHand(GameObject card, int playerId)
    {
        if (IsCardInHand(card.transform)) return;

        card.transform.localScale = new Vector3(1, 1, 1);
        card.transform.localEulerAngles = new Vector3(0, 180, 0);
        switch (playerId)
        {
            case 0:
                CardsInHandPlayer1.Add(card.transform);
                Vector3 offset1 = new Vector3(CardsInHandPlayer1.IndexOf(card.transform) * -4f, 0, 0.5f * CardsInHandPlayer1.IndexOf(card.transform));
                StartCoroutine(AnimationPlaceCardInHand(card.transform, HandPlayer1.position + offset1));
                break;
            case 1:
                CardsInHandPlayer2.Add(card.transform);
                Vector3 offset2 = new Vector3(CardsInHandPlayer2.IndexOf(card.transform) * -4f, 0, 0.5f * CardsInHandPlayer2.IndexOf(card.transform));
                StartCoroutine(AnimationPlaceCardInHand(card.transform, HandPlayer2.position + offset2));
                break;
            case 2:
                CardsInHandPlayer3.Add(card.transform);
                Vector3 offset3 = new Vector3(CardsInHandPlayer3.IndexOf(card.transform) * -4f, 0, 0.5f * CardsInHandPlayer3.IndexOf(card.transform));
                StartCoroutine(AnimationPlaceCardInHand(card.transform, HandPlayer3.position + offset3));
                break;
        }
        card.GetComponent<RV_AC_Parent>().OnPickUp();
    }

    public void FocusOnCardInHand(Transform card)
    {
        //animation
        Vector3 cardOffset = new Vector3(GetListOfCard(card.transform).IndexOf(card.transform) * -4, 0, -1);
        card.transform.position = GetTransformOfList(GetListOfCard(card.transform)).GetChild(0).transform.position + cardOffset;
    }

    public void UnfocusOnCardInHand(Transform card)
    {
        //undoAnimation
        Transform HandTransform = GetTransformOfList(GetListOfCard(card.transform));
        Vector3 handPosition = HandTransform.position;
        Vector3 cardOffset = new Vector3(GetListOfCard(card.transform).IndexOf(card.transform) * -4, 0, 0.5f * GetListOfCard(card).IndexOf(card.transform));
        card.transform.position = handPosition + cardOffset;
    }

    public void DiscardCardInHand(GameObject card)
    {
        StartCoroutine(AnimationPlaceCardInHand(card.transform, DiscardTransform.position, 3));
        card.transform.eulerAngles = new Vector3(card.transform.eulerAngles.x, 0, card.transform.eulerAngles.z);
        RV_PickACardOnEndTour.Instance.DiscardsList.Add(card);
        GetListOfCard(card.transform).Remove(card.transform);
        OnDiscard.Invoke();
    }

    public IEnumerator AnimationPlaceCardInHand(Transform card, Vector3 destination, float duration = 0.5f)
    {
        float timer = 0;
        while (timer < duration)
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

    public bool IsCardInHand(Transform card)
    {
        if (CardsInHandPlayer1.Contains(card))
            return true;
        if (CardsInHandPlayer2.Contains(card))
            return true;
        if (CardsInHandPlayer3.Contains(card))
            return true;
        
        return false;
    }

    public Transform GetTransformOfList(List<Transform> CardsList)
    {
        if (CardsList == CardsInHandPlayer1)
            return HandPlayer1;
        if (CardsList == CardsInHandPlayer2)
            return HandPlayer2;
        if (CardsList == CardsInHandPlayer3)
            return HandPlayer3;

        return null;
    }

    public int GetPlayerFromList(List<Transform> CardsList)
    {
        if (CardsList == CardsInHandPlayer1)
            return 0;
        if (CardsList == CardsInHandPlayer2)
            return 1;
        if (CardsList == CardsInHandPlayer3)
            return 2;

        return 0;
    }
}
