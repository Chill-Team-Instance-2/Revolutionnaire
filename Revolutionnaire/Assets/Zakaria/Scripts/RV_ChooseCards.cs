using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RV_ChooseCards : MonoBehaviour
{
    public Image ImageCard;

    public int CardID = 0;
    public int CardType = 1;

    public int PlayerClass = 1;

    [Header("Revolt")]
    [SerializeField] public List<TextMeshProUGUI> TextRequirement;
    [SerializeField] public List<TextMeshProUGUI> TextInfluence;

    [Header("Action")]
    [SerializeField] public TextMeshProUGUI TextTitleAction;
    [SerializeField] public TextMeshProUGUI TextDescriptionAction;

    public void GiveCardID(int cardID, int cardType, int pfaction) //1 == action card 2 == revolt card
    {
        CardID = cardID;
        CardType = cardType;
        PlayerClass = pfaction;
        if (cardType == 1)
        {
            Transform card = RV_PickACardOnEndTour.Instance.ActionsCards[cardID].transform;
            ImageCard.sprite = card.GetComponent<RV_ActionCard>().spriteFront;

            TextTitleAction.text = card.GetComponent<RV_AC_Parent>().TitleText;

            switch (PlayerClass)
            {
                case 0:
                    TextDescriptionAction.text = card.GetComponent<RV_AC_Parent>().MilText;
                    break;
                case 1:
                    TextDescriptionAction.text = card.GetComponent<RV_AC_Parent>().ComText;
                    break;
                case 2:
                    TextDescriptionAction.text = card.GetComponent<RV_AC_Parent>().IntText;
                    break;
            }
        }
        else if (cardType == 2)
        {
            Transform card = RV_PickACardOnEndTour.Instance.RevoltsCards[cardID].transform;
            ImageCard.sprite = card.GetComponent<RV_RevoltCard>().spriteFront;

            for (int i = 0; i < TextRequirement.Count; i++)
            {
                TextRequirement[i].text = card.GetComponent<RV_RevoltCard>().JetRequirements[i].ToString();
                TextInfluence[i].text = card.GetComponent<RV_RevoltCard>().JetInfluences[i].ToString();
            }
            
        }
    }

    public void PutCardInActual()
    {
        RV_PickACardOnEndTour.Instance.ActualToDiscard();
        RV_PickACardOnEndTour.Instance.PickACard(CardID, CardType);
        transform.parent.gameObject.SetActive(false);
    }
}
