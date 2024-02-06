using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class RV_ActionCard_Holder : MonoBehaviour
{
    [SerializeField] public Transform HandPlayer1; 
    [SerializeField] public Transform HandPlayer2; 
    [SerializeField] public Transform HandPlayer3;

    public void PutCardInHand(GameObject card, int playerId)
    {
        switch (playerId)
        {
            case 0:
                card.transform.SetParent(HandPlayer1);
                break;
            case 1:
                card.transform.SetParent(HandPlayer2);
                break;
            case 2:
                card.transform.SetParent(HandPlayer3);
                break;
        }
    }

    public void ActivateCardInHand(GameObject card)
    {
        
    }

    public void FocusOnCardInHand(GameObject card)
    {

    }

    public void UnfocusOnCardInHand(GameObject card)
    {

    }
}
