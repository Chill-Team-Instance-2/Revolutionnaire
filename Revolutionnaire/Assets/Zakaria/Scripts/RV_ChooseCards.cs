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
    [SerializeField] public GameObject RevoltParent;
    [SerializeField] public List<TextMeshProUGUI> TextRequirement;
    [SerializeField] public List<TextMeshProUGUI> TextInfluence;

    [Header("Action")]
    [SerializeField] public GameObject ActionParent;
    [SerializeField] public TextMeshProUGUI TextTitleAction;
    [SerializeField] public TextMeshProUGUI TextDescriptionAction;

    [SerializeField] public TextMeshProUGUI TextFaction;
    [SerializeField] public Image ImageFaction;
    [SerializeField] public Sprite SpriteMil;
    [SerializeField] public Sprite SpriteCom;
    [SerializeField] public Sprite SpriteInt;

    public void GiveCardID(int cardID, int cardType, int pfaction) //1 == action card 2 == revolt card
    {
        CardID = cardID;
        CardType = cardType;
        PlayerClass = pfaction;
        if (cardType == 1)
        {
            ActionParent.SetActive(true);
            RevoltParent.SetActive(false);

            Transform card = RV_PickACardOnEndTour.Instance.ActionsCards[cardID].transform;
            ImageCard.sprite = card.GetComponent<RV_ActionCard>().spriteFront;

            TextTitleAction.text = card.GetComponent<RV_AC_Parent>().TitleText;

            switch (PlayerClass)
            {
                case 0:
                    TextDescriptionAction.text = card.GetComponent<RV_AC_Parent>().MilText;
                    TextFaction.text = "Milice";
                    ImageFaction.sprite = SpriteMil;
                    break;
                case 1:
                    TextDescriptionAction.text = card.GetComponent<RV_AC_Parent>().ComText;
                    TextFaction.text = "Commerçant";
                    ImageFaction.sprite = SpriteCom;
                    break;
                case 2:
                    TextDescriptionAction.text = card.GetComponent<RV_AC_Parent>().IntText;
                    TextFaction.text = "Intellectuel";
                    ImageFaction.sprite = SpriteInt;
                    break;
            }
        }
        else if (cardType == 2)
        {
            ActionParent.SetActive(false);
            RevoltParent.SetActive(true);

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
