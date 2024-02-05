using UnityEngine;

public class RV_ac_Card3 : MonoBehaviour
{
    private RV_GameManager gameManager;
    private int Turn;
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
                CardUsed = true;
                Turn = 0;
                gameManager.Multiplier -= 0.5f;
                break;
            case 1:
                gameManager.PickACardOnEndTour.PickACard();
                gameManager.PickACardOnEndTour.ActualToDiscard();
                //TODO : Redraw (anim)
                break;
            case 2:
                RV_DiceManager.Instance.DiceResult = RV_DiceManager.Instance.DiceResult * 2;
                break;
            default:
                break;
        }
    }

    public void CheckTurn()
    {
        if (CardUsed)
        {
            if (Turn == 5)
            {
                gameManager.Multiplier += 0.5f;
                gameManager.Multiplier += 1;
            }
            else if (Turn == 10)
            {
                gameManager.Multiplier -= 1;
            }
            Turn++;
        }
    }
    public void EndAction()
    {
        switch (gameManager.PlayersClass[gameManager.PlayerTurn])
        {
            case 0:
                CardUsed = false;
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
