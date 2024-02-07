using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RV_ActionCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Sprite spriteFront;
    [SerializeField] private Sprite spriteBack;

    [SerializeField] private Image imageFront;
    [SerializeField] private Image imageBack;

    public UnityEvent OnClick;

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
