using UnityEngine;
using UnityEngine.UI;

public class RV_ChooseCards : MonoBehaviour
{
    public Image ImageCard;

    public int CardID = 0;
    public int CardType = 1;

    public void GiveCardID(int cardID, int cardType) //1 == action card 2 == revolt card
    {
        CardID = cardID;
        CardType = cardType;
        if (cardType == 1)
        {
            Transform card = RV_PickACardOnEndTour.Instance.ActionsCards[cardID].transform;
            ImageCard.sprite = card.GetComponent<RV_ActionCard>().spriteFront;
        }
        else if (cardType == 2)
        {
            Transform card = RV_PickACardOnEndTour.Instance.RevoltsCards[cardID].transform;
            ImageCard.sprite = card.GetComponent<RV_RevoltCard>().spriteFront;
        }
    }

    public void PutCardInActual()
    {
        RV_PickACardOnEndTour.Instance.ActualToDiscard();
        RV_PickACardOnEndTour.Instance.PickACard(CardID, CardType);
        transform.parent.gameObject.SetActive(false);
    }
}
