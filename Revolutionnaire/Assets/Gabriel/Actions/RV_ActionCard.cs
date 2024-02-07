using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RV_ActionCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Material materialFront;
    [SerializeField] private Material materialBack;

    [SerializeField] private MeshRenderer planeFront;
    [SerializeField] private MeshRenderer planeBack;

    //[SerializeField] private Sprite spriteFront;
    //[SerializeField] private Sprite spriteBack;

    //[SerializeField] private Image imageFront;
    //[SerializeField] private Image imageBack;

    [SerializeField] private Image imagePlayerClass;
    [SerializeField] private TextMeshProUGUI textCardID;

    [SerializeField] private Sprite spriteInt;
    [SerializeField] private Sprite spriteCom;
    [SerializeField] private Sprite spriteMil;


    public UnityEvent OnClick;

    public void Awake()
    {
        //RefreshVisual();
    }

    public void RefreshVisual()
    {
        RV_ActionCard_Holder hand = RV_ActionCard_Holder.Instance;
        if (hand.IsCardInHand(transform))
        {
            switch (hand.GetPlayerFromList(hand.GetListOfCard(transform)))
            {
                case 0:
                    imagePlayerClass.sprite = spriteMil;
                    break;
                case 1:
                    imagePlayerClass.sprite = spriteCom;
                    break;
                case 2:
                    imagePlayerClass.sprite = spriteInt;
                    break;
            }
        }
        else
        {
            switch (RV_GameManager.Instance.PlayerTurn)
            {
                case 0:
                    imagePlayerClass.sprite = spriteMil;
                    break;
                case 1:
                    imagePlayerClass.sprite = spriteCom;
                    break;
                case 2:
                    imagePlayerClass.sprite = spriteInt;
                    break;
            }
        }
        textCardID.text = GetComponent<RV_AC_Parent>().CardID.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (RV_ActionCard_Holder.Instance.IsCardInHand(transform)) //TODO || actual card
        {
            GetComponent<RV_AC_Parent>().Action();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (RV_ActionCard_Holder.Instance.IsCardInHand(transform))
        {
            RV_ActionCard_Holder.Instance.FocusOnCardInHand(transform);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (RV_ActionCard_Holder.Instance.IsCardInHand(transform))
        {
            RV_ActionCard_Holder.Instance.UnfocusOnCardInHand(transform);
        }
    }
}
