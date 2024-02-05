using UnityEngine;

public class RV_AC_Card3 : MonoBehaviour
{
    private RV_GameManager gameManager;
    private bool CardUsed = false;
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
    }
    public void Action()
    {
        switch (gameManager.PlayersClass[gameManager.PlayerTurn])
        {
            case 0:
                //TODO : Event pendant 4 tours puis event pendant 5 tours quand on aura la possibilité de gérer les tours
                break;
            case 1:
                gameManager.PickACardOnEndTour.PickACard();
                gameManager.PickACardOnEndTour.ActualToDiscard();
                //TODO : Redraw (anim)
                break;
            case 2:
                //diceManager.diceResult = diceManager.diceResult * 2;
                break;
            default:
                break;
        }
    }
    public void EndAction()
    {
        switch (gameManager.PlayersClass[gameManager.PlayerTurn])
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}
